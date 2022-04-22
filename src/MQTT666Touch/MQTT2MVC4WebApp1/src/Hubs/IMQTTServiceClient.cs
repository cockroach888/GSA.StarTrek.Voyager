namespace MQTT2MVC4WebApp1.Hubs;

public interface IMQTTServiceClient
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="topic">消息主题</param>
    /// <param name="message">消息内容</param>
    /// <returns>Task</returns>
    Task ReceiveMessageAsync(string topic, string message);
}
