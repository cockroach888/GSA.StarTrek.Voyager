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

namespace MQTT2MVC4WebApp1.Internal
{
    //using MqttMessageQueue = BlockingCollection<MessageModel>;

    internal class MQTTClientHostedServer : BackgroundService
    {
        private const int _maxParallelNumber = 100;
        private readonly BlockingCollection<MessageModel> _messageQueue = new();
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
                                         //.WithTcpServer("127.0.0.1")
                                         .WithTcpServer("192.168.16.221")
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

            await base.StopAsync(cancellationToken).ConfigureAwait(false);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() =>
            {
                _logger.LogInformation("MQTTClientHostedServer 后台异步执行方法 ExecuteAsync 收到停止令牌通知。");
            });


            //TDengineAPIContext.Default.Delete(new CheckItemsModel());

            //await TDengineAPIContext.Default.Update().ConfigureAwait(false);


            await Task.Delay(0).ConfigureAwait(false);

            //await MQTTConnectionAsync(stoppingToken).ConfigureAwait(false);

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    Parallel.For(0, _maxParallelNumber, async (i) =>
            //    {
            //        MessageModel message = _messageQueue.Take();

            //        await SaveMessageAsync(message).ConfigureAwait(false);

            //        string strMessage = Encoding.UTF8.GetString(message.Payload);
            //        await _mqttHub.Clients.All.ReceiveMessageAsync(message.Topic, strMessage).ConfigureAwait(false);
            //    });
            //}
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

        private async Task MessageReceivedCallback(MqttApplicationMessageReceivedEventArgs e)
        {
            MqttApplicationMessage msg = e.ApplicationMessage;
            if (msg != null && msg.Payload != null && msg.Payload.Length > 0)
            {
                _messageQueue.Add(MessageModel.New(msg.Topic, msg.Payload));
            }
            await Task.Delay(1).ConfigureAwait(false);
        }

        private async Task SaveMessageAsync(MessageModel message)
        {
            CheckItemsModel? info = JsonSerializer.Deserialize<CheckItemsModel>(message.Payload);
            await TDengineAPIContext.Default.Insert(info!);
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
