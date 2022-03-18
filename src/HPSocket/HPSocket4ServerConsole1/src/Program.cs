using HPSocket;
using HPSocket.Tcp;


Console.Title = "基于HP-Socket实现的示例程序 - 服务端";
uint chunkSize = 1024 * 32; // 32KB


Console.WriteLine("创建服务端。");
using ITcpPackServer server = new TcpPackServer()
{
    SocketBufferSize = chunkSize,

    MaxPackSize = chunkSize,
    PackHeaderFlag = 0x01,

    Address = "0.0.0.0",
    Port = 5257
};

// 绑定服务端响应事件
server.OnPrepareListen += OnPrepareListen;
server.OnAccept += OnAccept;
server.OnReceive += OnReceive;
server.OnClose += OnClose;
server.OnShutdown += OnShutdown;

// 启动服务
if (!server.Start())
{
    Console.WriteLine($"error code: {server.ErrorCode}, error message: {server.ErrorMessage}");
}

// 等待服务停止
await server.WaitAsync().ConfigureAwait(false);


Console.WriteLine("成功创建服务端，地址：[0.0.0.0:5257]。");
Console.WriteLine("--------------------------------------------------");
Console.WriteLine();


Console.WriteLine("等待读取输入的按键：");
Console.WriteLine("1.输入Q退出");
Console.WriteLine();

while (true)
{
    ConsoleKeyInfo key = Console.ReadKey();

    if (key.Key == ConsoleKey.Q)
    {
        await server.StopAsync().ConfigureAwait(false);
        break;
    }

    //switch (key.Key)
    //{
    //    // do something.
    //}

    Console.WriteLine();
    Console.WriteLine("等待读取输入的按键：");
    Console.WriteLine("1.输入Q退出");
    Console.WriteLine();
}





// ReSharper disable once InconsistentNaming
HandleResult OnPrepareListen(IServer sender, IntPtr listen)
{
    Console.WriteLine($"OnPrepareListen({sender.Address}:{sender.Port}), listen: {listen}, hp-socket version: {sender.Version}");
    Console.WriteLine();

    return HandleResult.Ok;
}

// ReSharper disable once InconsistentNaming
HandleResult OnAccept(IServer sender, IntPtr connId, IntPtr client)
{
    // 获取客户端地址
    if (!sender.GetRemoteAddress(connId, out var ip, out var port))
    {
        return HandleResult.Error;
    }

    Console.WriteLine($"OnAccept({connId}), ip: {ip}, port: {port}");
    Console.WriteLine();

    return HandleResult.Ok;
}

// ReSharper disable once InconsistentNaming
HandleResult OnReceive(IServer sender, IntPtr connId, byte[] data)
{
    Console.WriteLine($"OnReceive({connId}), data length: {data.Length}");
    Console.WriteLine();

    return OnProcessFullPacket(sender, connId, data);
}

// ReSharper disable once InconsistentNaming
HandleResult OnClose(IServer sender, IntPtr connId, SocketOperation socketOperation, int errorCode)
{
    Console.WriteLine($"OnClose({connId}), socket operation: {socketOperation}, error code: {errorCode}");
    Console.WriteLine();

    return HandleResult.Ok;
}

// ReSharper disable once InconsistentNaming
HandleResult OnShutdown(IServer sender)
{
    Console.WriteLine($"OnShutdown({sender.Address}:{sender.Port})");
    Console.WriteLine();

    return HandleResult.Ok;
}

// ReSharper disable once InconsistentNaming
HandleResult OnProcessFullPacket(IServer sender, IntPtr connId, byte[] data)
{
    Console.WriteLine($"数据包具体解析处理过程。");
    Console.WriteLine();

    // D:\data\ImageUpload

    return HandleResult.Ok;
}
