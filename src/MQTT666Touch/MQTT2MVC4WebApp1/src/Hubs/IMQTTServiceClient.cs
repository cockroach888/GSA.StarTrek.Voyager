namespace MQTT2MVC4WebApp1.Hubs;

public interface IMQTTServiceClient
{
    /// <summary>
    /// 发送消息到所有客户端
    /// </summary>
    /// <param name="topic">消息主题</param>
    /// <param name="message">消息内容</param>
    /// <returns>Task</returns>
    Task ReceiveMessage(string topic, string message);
}
