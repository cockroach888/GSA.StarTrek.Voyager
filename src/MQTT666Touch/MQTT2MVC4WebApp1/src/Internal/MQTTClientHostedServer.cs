namespace MQTT2MVC4WebApp1.Internal
{
    public class MQTTClientHostedServer : BackgroundService
    {
        private readonly ILogger<MQTTClientHostedServer> _logger;


        public MQTTClientHostedServer(ILogger<MQTTClientHostedServer> logger)
        {
            _logger = logger;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("连接服务端。");



        }
    }
}
