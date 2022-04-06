using Microsoft.AspNetCore.SignalR;

namespace MQTT2MVC4WebApp1.Hubs;

internal class MQTTServiceHub : Hub<IMQTTServiceClient>
{
    /// <summary>
    /// 发送消息到所有客户端
    /// </summary>
    /// <param name="topic">消息主题</param>
    /// <param name="message">消息内容</param>
    /// <returns>Task</returns>
    public async Task SendMessage(string topic, string message)
        => await Clients.All.ReceiveMessage(topic, message);

    /// <summary>
    /// 发送消息到所有客户端
    /// </summary>
    /// <remarks>Gets a caller to the connection which triggered the current invocation.</remarks>
    /// <param name="topic">消息主题</param>
    /// <param name="message">消息内容</param>
    /// <returns>Task</returns>
    public async Task SendMessageToCaller(string topic, string message)
        => await Clients.Caller.ReceiveMessage(topic, message);

    /// <summary>
    /// 发送消息到所有客户端
    /// </summary>
    /// <remarks>Gets a T that can be used to invoke methods on all connections in the specified group.</remarks>
    /// <param name="topic">消息主题</param>
    /// <param name="message">消息内容</param>
    /// <returns>Task</returns>
    public async Task SendMessageToGroup(string topic, string message)
        => await Clients.Group("SignalR Users").ReceiveMessage(topic, message);
}
