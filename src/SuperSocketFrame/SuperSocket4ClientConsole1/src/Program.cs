using SuperSocket.Client;
using SuperSocket4ClientConsole1;
using System.Net;
using System.Text;

var client = new EasyClient<MyPackage>(new MyPackageFilter()).AsClient();

if (!await client.ConnectAsync(new IPEndPoint(IPAddress.Loopback, 5257)))
{
    Console.WriteLine("Failed to connect the target server.");
    return;
}

await client.SendAsync(Encoding.UTF8.GetBytes("This is a test message.")).ConfigureAwait(false);

while (true)
{
    var p = await client.ReceiveAsync();

    if (p == null) // connection dropped
        break;

    Console.WriteLine(p.Body);
}
