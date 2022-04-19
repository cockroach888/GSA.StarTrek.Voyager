namespace MQTT2MVC4WebApp1.KnowArgs;

/// <summary>
/// 消息投递事件参数类
/// </summary>
internal class MessageDespatchKnowArgs : EventArgs
{
    /// <summary>
    /// 消息投递事件参数
    /// </summary>
    /// <param name="topic">消息主题</param>
    /// <param name="message">消息内容</param>
    public MessageDespatchKnowArgs(string topic, string message)
    {
        Topic = topic;
        Message = message;
    }


    /// <summary>
    /// 消息主题
    /// </summary>
    public string Topic { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public string Message { get; set; }


    /// <summary>
    /// 新建一个消息投递事件参数实例
    /// </summary>
    /// <param name="topic">消息主题</param>
    /// <param name="message">消息内容</param>
    /// <returns>消息投递事件参数</returns>
    public static MessageDespatchKnowArgs New(string topic, string message)
        => new(topic, message);
}
