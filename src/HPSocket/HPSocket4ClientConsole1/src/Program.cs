using HPSocket;
using HPSocket.Tcp;
using HPSocket4ClientConsole1;

Console.Title = "基于HP-Socket实现的示例程序 - 客户端";


Console.WriteLine("创建客户端。");
using ITcpClient client = new TcpClient()
{
    SocketBufferSize = 4096,
    Async = true,
    Address = "127.0.0.1",
    Port = 5257
};

// 绑定客户端响应事件
client.OnPrepareConnect += OnPrepareConnect;
client.OnConnect += OnConnect;
client.OnReceive += OnReceive;
client.OnClose += OnClose;

// 连接到目标服务器
if (!client.Connect())
{
    Console.WriteLine($"error code: {client.ErrorCode}, error message: {client.ErrorMessage}");
}

// 等待服务停止
//await client.WaitAsync().ConfigureAwait(false);


Console.WriteLine("成功创建客户端。");
Console.WriteLine("--------------------------------------------------");
Console.WriteLine();

await new LargeImageSendTest().StartAsync(client).ConfigureAwait(false);


Console.WriteLine("等待读取输入的信息：");
Console.WriteLine("1.输入Q退出");
Console.WriteLine();

while (true)
{
    string? inputString = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(inputString))
    {
        continue;
    }

    if (inputString.Contains('Q', StringComparison.OrdinalIgnoreCase))
    {
        await client.StopAsync().ConfigureAwait(false);
        break;
    }

    byte[] buffer = System.Text.UTF8Encoding.UTF8.GetBytes(inputString);
    client.Send(buffer, buffer.Length);

    Console.WriteLine();
    Console.WriteLine("等待读取输入的信息：");
    Console.WriteLine("1.输入Q退出");
    Console.WriteLine();
}





// ReSharper disable once InconsistentNaming
HandleResult OnPrepareConnect(IClient sender, IntPtr socket)
{
    Console.WriteLine($"OnPrepareConnect({sender.Address}:{sender.Port}), socket handle: {socket}, hp-socket version: {sender.Version}");
    Console.WriteLine();

    return HandleResult.Ok;
}

// ReSharper disable once InconsistentNaming
HandleResult OnConnect(IClient sender)
{
    Console.WriteLine("OnConnect()");
    Console.WriteLine();

    return HandleResult.Ok;
}

// ReSharper disable once InconsistentNaming
HandleResult OnReceive(IClient sender, byte[] data)
{
    Console.WriteLine($"OnReceive(), data length: {data.Length}");
    Console.WriteLine();

    return OnProcessFullPacket(data);
}

// ReSharper disable once InconsistentNaming
HandleResult OnClose(IClient sender, SocketOperation socketOperation, int errorCode)
{
    Console.WriteLine($"OnClose(), socket operation: {socketOperation}, error code: {errorCode}");
    Console.WriteLine();

    return HandleResult.Ok;
}

// ReSharper disable once InconsistentNaming
HandleResult OnProcessFullPacket(byte[] data)
{
    Console.WriteLine($"数据包具体解析处理过程。");
    Console.WriteLine();

    return HandleResult.Ok;
}
