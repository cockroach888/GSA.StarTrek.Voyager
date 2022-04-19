namespace MQTT2MVC4WebApp1.Services;

/// <summary>
/// 消息投递服务
/// </summary>
internal class MessageDespatchService
{
    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<MessageDespatchService> _logger;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public MessageDespatchService(ILogger<MessageDespatchService> logger)
        => _logger = logger;


    /// <summary>
    /// 消息投递事件
    /// </summary>
    public event Func<string, string, Task>? MessageDespatch;


    /// <summary>
    /// 通知消息投递事件
    /// </summary>
    /// <param name="topic">消息主题</param>
    /// <param name="message">消息内容</param>
    public async Task NotifyMessageDespatchAsync(string topic, string message)
    {
        _logger.LogInformation($"收到来自网页的消息投递：{topic} - {message}");
        await MessageDespatch!.Invoke(topic, message).ConfigureAwait(false);
    }
}
