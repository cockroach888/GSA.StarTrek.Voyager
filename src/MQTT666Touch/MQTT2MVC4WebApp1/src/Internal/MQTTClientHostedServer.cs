using Microsoft.AspNetCore.SignalR;
using MQTT2MVC4WebApp1.Hubs;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Diagnostics;
using MQTTnet.Exceptions;
using System.Collections.Concurrent;
using System.Text;

namespace MQTT2MVC4WebApp1.Internal
{
    internal class MQTTClientHostedServer : BackgroundService
    {
        private readonly BlockingCollection<MqttApplicationMessage> _messageQueue = new();
        private readonly ILogger<MQTTClientHostedServer> _logger;
        private readonly IHubContext<MQTTServiceHub, IMQTTServiceClient> _mqttHub;
        private readonly MqttFactory _mqttFactory;
        private MqttClientOptions? _clientOptions;
        private readonly MqttClient _mqttClient;


        public MQTTClientHostedServer(ILogger<MQTTClientHostedServer> logger, IHubContext<MQTTServiceHub, IMQTTServiceClient> mqttHub)
        {
            _logger = logger;
            _mqttHub = mqttHub;

            _logger.LogDebug("实例化MQTT创建工厂和客户端对象。");
            IMqttNetLogger mqttLogger = new MqttNetEventLogger();
            _mqttFactory = new MqttFactory(mqttLogger);
            _mqttClient = _mqttFactory.CreateMqttClient();
        }


        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("注册消息接收回调事件。");
            _mqttClient.ApplicationMessageReceivedAsync += MessageReceivedCallback;


            _logger.LogInformation("注册客户端连接回调事件。");
            _mqttClient.ConnectedAsync += async (e) =>
             {
                 _logger.LogInformation($"Client Id: {e.ConnectResult.ResultCode}");


                 _logger.LogInformation("订阅主题。");
                 MqttClientSubscribeOptions mqttSubscribeOptions = _mqttFactory.CreateSubscribeOptionsBuilder()
                                                                               .WithTopicFilter(f => { f.WithTopic("testtopic/#"); })
                                                                               .Build();

                 await _mqttClient.SubscribeAsync(mqttSubscribeOptions, cancellationToken).ConfigureAwait(false);
             };


            _logger.LogInformation("构建服务端连接参数信息。");
            _clientOptions = _mqttFactory.CreateClientOptionsBuilder()
                                         //.WithTcpServer("127.0.0.1")
                                         .WithTcpServer("192.168.16.221")
                                         .WithClientId($"ClientWPF1")
                                         .WithCredentials("test", "test")
                                         .Build();


            await base.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            MqttClientDisconnectOptions options = _mqttFactory.CreateClientDisconnectOptionsBuilder().Build();
            await _mqttClient.DisconnectAsync(options, cancellationToken).ConfigureAwait(false);
            _mqttClient.Dispose();

            using (_messageQueue) { };

            await base.StopAsync(cancellationToken).ConfigureAwait(false);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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
                finally
                {
                    _logger.LogDebug("程序将在5秒后尝试重新连接服务端。");
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken).ConfigureAwait(false);
                }
            }


            while (!stoppingToken.IsCancellationRequested)
            {
                //_messageQueue.TryTake(out MqttApplicationMessage message, -1, stoppingToken);
                MqttApplicationMessage message = _messageQueue.Take(stoppingToken);

                string strMessage = Encoding.UTF8.GetString(message.Payload);
                _logger.LogDebug($"Topic: {message.Topic}");
                _logger.LogDebug($"Payload: {strMessage}");

                await _mqttHub.Clients.All.ReceiveMessage(message.Topic, strMessage).ConfigureAwait(false);
            }
        }


        private Task MessageReceivedCallback(MqttApplicationMessageReceivedEventArgs e)
        {
            return Task.Run(() =>
            {
                _messageQueue.Add(e.ApplicationMessage);
            });
        }
    }
}
