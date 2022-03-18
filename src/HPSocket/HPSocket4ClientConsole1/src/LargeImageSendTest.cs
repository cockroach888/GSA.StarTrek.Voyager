using HPSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HPSocket4ClientConsole1
{
    internal class LargeImageSendTest
    {
        public async Task StartAsync(ITcpPackClient client)
        {
            byte[] testBuffer = Encoding.UTF8.GetBytes("test");
            bool tmpResult = client.Send(testBuffer, testBuffer.Length);


            string[] files = new string[] {
                "E:/data/TestImage/test01.jpg",
                "E:/data/TestImage/test02.jpg",
                "E:/data/TestImage/HYP_LR.tif"
                //"E:/data/TestImage/HYP_LR_SR_W.tif",
                //"E:/data/TestImage/HYP_LR_SR_W_DR.tif",
                //"E:/data/TestImage/HYP_LR_SR_OB_DR.tif",
                //"E:/data/TestImage/HYP_LR_SR.tif",
                //"E:/data/TestImage/HYP_HR.tif",
                //"E:/data/TestImage/HYP_HR_SR_W.tif",
                //"E:/data/TestImage/HYP_HR_SR_W_DR.tif",
                //"E:/data/TestImage/HYP_HR_SR_OB_DR.tif",
                //"E:/data/TestImage/HYP_HR_SR.tif"
            };



            foreach (string file in files)
            {
                //int chunkSize = 1024 * 32; // 32KB
                //var buffer = new byte[chunkSize];

                Stopwatch stopwatch = Stopwatch.StartNew();

                bool sendResult = client.SendSmallFile(file, null, null);
                if (!sendResult)
                {
                    Console.WriteLine(client.ErrorMessage);
                }

                //await using var fs = File.OpenRead(file);

                //Wsabuf[] wsabufs = new Wsabuf[fs.Length / chunkSize + 1];
                //int index = 0;

                //while (true)
                //{
                //    int count = await fs.ReadAsync(buffer).ConfigureAwait(false);
                //    if (count <= 0)
                //    {
                //        break;
                //    }

                //    //client.Send(buffer, buffer.Length);


                //    IntPtr unmanagedPointer = Marshal.AllocHGlobal(count);
                //    Marshal.Copy(buffer, 0, unmanagedPointer, count);
                //    // Call unmanaged code
                //    Marshal.FreeHGlobal(unmanagedPointer);

                //    try
                //    {
                //        wsabufs[index] = new Wsabuf()
                //        {
                //            Buffer = unmanagedPointer,
                //            Length = count
                //        };
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine(ex.Message);
                //    }

                //    index++;
                //}

                //bool sendResult = client.SendPackets(wsabufs, wsabufs.Length);
                //if (!sendResult)
                //{
                //    Console.WriteLine(client.ErrorMessage);
                //}


                stopwatch.Stop();

                //Console.WriteLine("File size: {0} bytes", fs.Length);
                Console.WriteLine("Elapsed  : {0} ms", stopwatch.ElapsedMilliseconds);
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine();
            }
        }
    }
}
