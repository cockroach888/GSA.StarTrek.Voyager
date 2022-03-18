using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gPRC4ClientConsole1
{
    internal class FileReadTest
    {
        public async Task StartAsync()
        {
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



            foreach (string file in files)
            {
                var buffer = new byte[1024 * 32];

                Stopwatch stopwatch = Stopwatch.StartNew();

                Console.WriteLine($"Read file {file} to stream.");

                await using var fs = File.OpenRead(file);

                long hash = 0;
                while (await fs.ReadAsync(buffer) > 0)
                {
                    hash += buffer.Length;                    
                }

                stopwatch.Stop();

                Console.WriteLine("File size: {0} bytes", fs.Length);
                Console.WriteLine("Elapsed  : {0} ms", stopwatch.ElapsedMilliseconds);
                Console.WriteLine("Checksum : {0}", hash);
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine();
            }


            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
