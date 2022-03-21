using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperSocket;
using SuperSocket.ProtoBase;
using System.Text;


IHost host = SuperSocketHostBuilder.Create<TextPackageInfo, LinePipelineFilter>(args)
    .UsePackageHandler(async (session, package) =>
    {
        await session.SendAsync(Encoding.UTF8.GetBytes($"{package.Text}\r\n"));
    })
    .ConfigureSuperSocket(option =>
    {
        option.Name = "SuperSocket Test Server";
        option.AddListener(new ListenOptions
        {
            Ip = "Any",
            Port = 5257
        });
    })
    .ConfigureLogging((hostCtx, loggingBuilder) =>
    {
        loggingBuilder.AddConsole();
    })
    .Build();

await host.RunAsync().ConfigureAwait(false);
