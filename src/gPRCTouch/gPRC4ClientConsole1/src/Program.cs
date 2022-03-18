using Google.Protobuf;
using gPRC4ClientConsole1;
using Grpc.Net.Client;
using SightX2gRPC;
using System.Diagnostics;
using System.IO;



//await new FileReadTest().StartAsync();

Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine();


int chunkSize = 1024 * 1024; // 1 MB

using var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new Uploader.UploaderClient(channel);


string[] files = new string[] {
    "D:/data/TestImage/test01.jpg",
    "D:/data/TestImage/test02.jpg",
    "D:/data/TestImage/test10MB.jpg",
    "D:/data/TestImage/test20MB.jpg",
    "D:/data/TestImage/test30MB.jpg",
    "D:/data/TestImage/test40MB.jpg",
    "D:/data/TestImage/test50MB.jpg",
    "D:/data/TestImage/test60MB.jpg",
    "D:/data/TestImage/test70MB.jpg",
    "D:/data/TestImage/test80MB.jpg",
    "D:/data/TestImage/test90MB.jpg",
    "D:/data/TestImage/test100MB.jpg"
};


var buffer = new byte[chunkSize];

Stopwatch stopwatch = new();

foreach (string file in files)
{
    stopwatch.Restart();

    Console.WriteLine("Starting call");
    var call = client.UploadFile();

    Console.WriteLine($"Read file {file} to stream.");

    await using var readStream = File.OpenRead(file);

    //Console.WriteLine($"Sending file metadata. file size: {readStream.Length}.");
    //await call.RequestStream.WriteAsync(new UploadFileRequest
    //{
    //    Metadata = new FileMetadata
    //    {
    //        FileName = Path.GetFileName(file),
    //        FileSize = readStream.Length
    //    }
    //});

    while (true)
    {
        var count = await readStream.ReadAsync(buffer);
        if (count == 0)
        {
            break;
        }

        await call.RequestStream.WriteAsync(new UploadFileRequest
        {
            Data = UnsafeByteOperations.UnsafeWrap(buffer.AsMemory(0, count))
        });
    }

    Console.WriteLine("Complete request");
    await call.RequestStream.CompleteAsync();

    var response = await call;
    Console.WriteLine("Upload id: " + response.Id);

    stopwatch.Stop();


    Console.WriteLine("File size: {0} bytes", readStream.Length);
    Console.WriteLine("Elapsed  : {0} ms", stopwatch.ElapsedMilliseconds);
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine();
}



Console.WriteLine("Shutting down");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
