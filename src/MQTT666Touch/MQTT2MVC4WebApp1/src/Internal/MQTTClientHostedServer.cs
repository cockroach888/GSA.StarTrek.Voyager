using Microsoft.AspNetCore.SignalR;
using MQTT2MVC4WebApp1.Hubs;
using MQTT2MVC4WebApp1.Models;
using MQTT2MVC4WebApp1.Services;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Diagnostics;
using MQTTnet.Exceptions;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using TDengineDriver;

namespace MQTT2MVC4WebApp1.Internal
{
    using MqttMessageQueue = BlockingCollection<MqttApplicationMessage>;

    internal class MQTTClientHostedServer : BackgroundService
    {
        private const int _maxParallelNumber = 1000;

        private IntPtr[] _connections = new IntPtr[_maxParallelNumber];

        private readonly MqttMessageQueue _messageQueue = new();
        private readonly ILogger<MQTTClientHostedServer> _logger;
        private readonly IConfiguration _config;
        private readonly IHubContext<MQTTServiceHub, IMQTTServiceClient> _mqttHub;
        private readonly MqttFactory _mqttFactory;
        private MqttClientOptions? _clientOptions;
        private readonly MqttClient _mqttClient;
        private readonly MessageDespatchService _despatchService;


        public MQTTClientHostedServer(ILogger<MQTTClientHostedServer> logger,
                                      IConfiguration config,
                                      IHubContext<MQTTServiceHub,
                                      IMQTTServiceClient> mqttHub,
                                      MessageDespatchService messageDespatchService)
        {
            //TDengine.Options((int)TDengineInitOption.TDDB_OPTION_CONFIGDIR, ".");

            for (int i = 0; i < _maxParallelNumber; i++)
            {
                _connections[i] = TDengine.Connect("192.168.16.221", "root", "taosdata", "EdgeDetectionDB", 0);
            }

            _logger = logger;
            _config = config;
            _mqttHub = mqttHub;
            _despatchService = messageDespatchService;

            _logger.LogDebug("实例化MQTT创建工厂和客户端对象。");
            IMqttNetLogger mqttLogger = new MqttNetEventLogger();
            _mqttFactory = new MqttFactory(mqttLogger);
            _mqttClient = _mqttFactory.CreateMqttClient();
        }


        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _despatchService.MessageDespatch += ReceiveMessageAsync;

            _logger.LogInformation("注册消息接收回调事件。");
            _mqttClient.ApplicationMessageReceivedAsync += MessageReceivedCallback;


            _logger.LogInformation("注册客户端连接回调事件。");
            _mqttClient.ConnectedAsync += async (e) =>
             {
                 _logger.LogInformation($"Client Id: {e.ConnectResult.ResultCode}");


                 _logger.LogInformation("订阅主题。");
                 MqttClientSubscribeOptions mqttSubscribeOptions = _mqttFactory.CreateSubscribeOptionsBuilder()
                                                                               .WithTopicFilter(p => { p.WithTopic("testtopic/#"); })
                                                                               .WithTopicFilter(p => { p.WithTopic("deivce/+/checkItems"); })
                                                                               .Build();

                 await _mqttClient.SubscribeAsync(mqttSubscribeOptions, cancellationToken).ConfigureAwait(false);
             };

            _mqttClient.DisconnectedAsync += async (e) =>
            {
                _logger.LogDebug($"MQTT 连接中断。");

                //_logger.LogDebug($"ResultCode: {e.ConnectResult.ResultCode}");
                //_logger.LogDebug($"ReasonString: {e.ReasonString}");
                //_logger.LogError(e.Exception, e.Exception.Message);

                _logger.LogInformation($"尝试重新连接到服务端。");
                await MQTTConnectionAsync().ConfigureAwait(false);
            };


            _logger.LogInformation("构建服务端连接参数信息。");
            _clientOptions = _mqttFactory.CreateClientOptionsBuilder()
                                         .WithTcpServer("127.0.0.1")
                                         //.WithTcpServer("192.168.16.221")
                                         .WithCredentials("test", "test")
                                         .WithClientId($"AspDotNetCoreClient")
                                         .WithKeepAlivePeriod(TimeSpan.FromSeconds(60))
                                         .WithSessionExpiryInterval(0)
                                         .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V500)
                                         .WithCommunicationTimeout(TimeSpan.FromHours(1))
                                         .WithKeepAlivePeriod(TimeSpan.FromMinutes(1))
                                         .WithCleanSession(true)
                                         .Build();


            await base.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _despatchService.MessageDespatch -= ReceiveMessageAsync;


            MqttClientDisconnectOptions options = _mqttFactory.CreateClientDisconnectOptionsBuilder().Build();
            await _mqttClient.DisconnectAsync(options, cancellationToken).ConfigureAwait(false);
            _mqttClient.Dispose();

            using (_messageQueue) { };

            // 释放所有连接
            foreach (IntPtr conn in _connections)
            {
                _ = TDengine.Close(conn);
                TDengine.Cleanup();
            }

            await base.StopAsync(cancellationToken).ConfigureAwait(false);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() =>
            {
                _logger.LogInformation("MQTTClientHostedServer 后台异步执行方法 ExecuteAsync 收到停止令牌通知。");
            });

            await MQTTConnectionAsync(stoppingToken).ConfigureAwait(false);

            while (!stoppingToken.IsCancellationRequested)
            {
                //MqttApplicationMessage message = _messageQueue.Take();
                //string strMessage = Encoding.UTF8.GetString(message.Payload);
                //await _mqttHub.Clients.All.ReceiveMessageAsync(message.Topic, strMessage).ConfigureAwait(false);

                Parallel.For(0, _maxParallelNumber, async (i) =>
                {
                    MqttApplicationMessage message = _messageQueue.Take();
                    if (message != null && message.Payload != null && message.Payload.Length > 0)
                    {
                        await SaveMessageAsync(_connections[i], message).ConfigureAwait(false);

                        string strMessage = Encoding.UTF8.GetString(message.Payload);
                        await _mqttHub.Clients.All.ReceiveMessageAsync(message.Topic, strMessage).ConfigureAwait(false);
                    }
                });
                //await Task.Delay(0, stoppingToken).ConfigureAwait(false);
            }
            //await Task.Delay(0, stoppingToken).ConfigureAwait(false);
        }

        private async Task MQTTConnectionAsync(CancellationToken stoppingToken = default)
        {
            while (true)
            {
                try
                {
                    _logger.LogInformation("尝试连接到服务端。");
                    MqttClientConnectResult response = await _mqttClient.ConnectAsync(_clientOptions, stoppingToken).ConfigureAwait(false);
                    break;
                }
                catch (MqttCommunicationException ex)
                {
                    _logger.LogError(ex, ex.Message);
                    _logger.LogDebug("服务端连接失败，请确认服务端工作是否正常！");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    _logger.LogDebug("服务端连接时出现未知错误，请检查连接参数，或呼叫管理员。");
                }

                _logger.LogDebug("程序将在5秒后尝试重新连接服务端。");
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken).ConfigureAwait(false);
            }
            //await Task.Delay(0, stoppingToken).ConfigureAwait(false);
        }

        private Task MessageReceivedCallback(MqttApplicationMessageReceivedEventArgs e)
        {
            return Task.Run(() => _messageQueue.Add(e.ApplicationMessage));
            //await Task.Delay(1).ConfigureAwait(false);
        }

        private Task SaveMessageAsync(IntPtr conn, MqttApplicationMessage message)
        {
            return Task.Run(() =>
            {
                CheckItemsModel? info = JsonSerializer.Deserialize<CheckItemsModel>(message.Payload);

                StringBuilder sqlString = new();

                sqlString.Append($"insert into edgedetectiondb.{info!.DeviceId} values (");
                sqlString.Append($"'{info.TimestampKey:yyyy-MM-dd HH:mm:ss.fff}',"); // ts timestamp
                sqlString.Append($"{info.ProductId},"); // product_id int
                sqlString.Append($"{info.GoodResult},"); // good_result int
                sqlString.Append($"{info.BadResult},"); // bad_result int
                sqlString.Append($"{info.ExceptionResult},"); // exception_result int
                sqlString.Append($"'{info.Note}',"); // note nchar(1000)
                sqlString.Append($"'{info.SavePath}',"); // save_path nchar(200)
                sqlString.Append($"{info.Data1},"); // d1 int
                sqlString.Append($"{info.Data2},"); // d2 int
                sqlString.Append($"{info.Data3},"); // d3 int
                sqlString.Append($"{info.Data4},"); // d4 int
                sqlString.Append($"{info.Data5},"); // d5 int
                sqlString.Append($"{info.Data6},"); // d6 int
                sqlString.Append($"{info.Data7},"); // d7 int
                sqlString.Append($"{info.Data8},"); // d8 int
                sqlString.Append($"{info.Data9},"); // d9 int
                sqlString.Append($"{info.Data10},"); // d10 int
                sqlString.Append($"{info.Data11},"); // d11 int
                sqlString.Append($"{info.Data12},"); // d12 int
                sqlString.Append($"{info.Data13},"); // d13 int
                sqlString.Append($"{info.Data14},"); // d14 int
                sqlString.Append($"{info.Data15},"); // d15 int
                sqlString.Append($"{info.Data16},"); // d16 int
                sqlString.Append($"{info.Data17},"); // d17 int
                sqlString.Append($"{info.Data18},"); // d18 int
                sqlString.Append($"{info.Data19},"); // d19 int
                sqlString.Append($"{info.Data20}"); // d20 int
                sqlString.Append(");");

                IntPtr result = TDengine.Query(conn, sqlString.ToString());
                TDengine.FreeResult(result);
            });
        }

        private async Task ReceiveMessageAsync(string topic, string message)
        {
            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(message)
                .Build();

            _logger.LogInformation($"收到来自网页的消息发布：{topic} - {message}");

            await _mqttClient.PublishAsync(applicationMessage, CancellationToken.None).ConfigureAwait(false);

            _logger.LogInformation($"来自网页的消息发布成功。");
        }
    }
}
