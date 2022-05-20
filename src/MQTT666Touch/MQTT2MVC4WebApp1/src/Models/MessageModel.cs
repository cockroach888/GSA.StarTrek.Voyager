namespace MQTT2MVC4WebApp1.Models;

/// <summary>
/// 消息数据模型
/// </summary>
[Serializable]
public sealed class MessageModel
{
    /// <summary>
    /// 消息数据模型
    /// </summary>
    /// <param name="topic">主题名称</param>
    /// <param name="payload">消息内容</param>
    public MessageModel(string topic, byte[] payload)
    {
        Topic = topic;
        Payload = payload;
    }


    /// <summary>
    /// 主题名称
    /// </summary>
    public string Topic { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public byte[] Payload { get; set; }


    /// <summary>
    /// 创建一个消息数据模型的新的实例
    /// </summary>
    /// <param name="topic">主题名称</param>
    /// <param name="payload">消息内容</param>
    /// <returns>消息数据模型</returns>
    public static MessageModel New(string topic, byte[] payload)
        => new MessageModel(topic, payload);
}
