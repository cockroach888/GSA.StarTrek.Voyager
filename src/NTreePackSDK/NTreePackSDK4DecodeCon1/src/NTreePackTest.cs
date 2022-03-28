using Microsoft.Win32.SafeHandles;
using NTreePackSDK4DecodeCon1.Common;
using NTreePackSDK4DecodeCon1.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using static NTreePackSDK4DecodeCon1.Core.NTreePackModuleDelegate;

namespace NTreePackSDK4DecodeCon1
{
    internal class NTreePackTest
    {
        //public delegate NTreePackHandleResult ExpPackToMemoryEventHandler(Memory<byte> data);

        //public event ExpPackToMemoryEventHandler OnExpPackToMemory;


        public NTreePackTest()
        {

        }


        public void StartAsync()
        {
            string filePath1 = "D:/data/plcm/TestData.plcm";
            string filePath2 = "D:/data/plcm/TestMoreData.plcm";


            //Stopwatch sw1 = Stopwatch.StartNew();

            //Memory<byte> tmpbuffer = await File.ReadAllBytesAsync(filePath1).ConfigureAwait(false);
            //FileOptions options = FileOptions.Asynchronous | (OperatingSystem.IsWindows() ? FileOptions.SequentialScan : FileOptions.None);
            //using SafeFileHandle sfh = File.OpenHandle(filePath1, options: options);

            //sw1.Stop();
            //Trace.TraceInformation($"{sw1.ElapsedMilliseconds}ms.");


            //ExpPackToMemoryCallback callback = OnExpPackToMemoryCallback;

            IntPtr filePointer = NTreePackModuleExtern.ExpCreatePackFile(filePath1);





            // IntPtr 调用
            ushort resultIntPtr = 999;
            unsafe
            {
                ushort* resultPointer = &resultIntPtr;

                NTreePackModuleExternTest.ExpGetWordB(filePointer, "Id", (IntPtr)resultPointer);
            }
            Trace.TraceInformation($"IntPtr Id: {resultIntPtr}");


            // 引用传递
            ushort resultRef = 999;
            NTreePackModuleExternTest.ExpGetWordB(filePointer, "Id", ref resultRef);
            Trace.TraceInformation($"ref Id: {resultRef}");


            // 不安全包装调用
            NTreePackModuleExternTestExtension.ExpGetWordB(filePointer, "Id", out ushort dataResult);
            Trace.TraceInformation($"unsafe Id: {dataResult}");







            //uint childCount = NTreePackModuleExtern.ExpGetChildCount(filePointer);

            //ushort result = 999;

            ////NTreePackModuleExtern.ExpGetWordB(filePointer, "Id", ref result);


            //IntPtr resultPointer = Marshal.AllocHGlobal(result);

            //NTreePackModuleExtern.ExpGetWordB(filePointer, "Id", resultPointer);

            //Marshal.FreeHGlobal(resultPointer);


            //unsafe
            //{
            //    NTreePackModuleExtern.ExpGetWordB(filePointer, "Id", &result);
            //}



            //GCHandle resultHandle = GCHandle.Alloc(result, GCHandleType.Pinned);

            //IntPtr resultPointer = resultHandle.AddrOfPinnedObject();
            //IntPtr resultPointer = GCHandle.ToIntPtr(resultHandle);



            //resultHandle.Free();

            //Trace.TraceInformation($"ChildCount: {childCount}");
            //Trace.TraceInformation($"Id: {result}");


            //unsafe
            //{
            //    ushort resultValue = 999;
            //    ushort* p = &resultValue;

            //    NTreePackModuleExtern.ExpGetWordB(dataSource, "Id", (IntPtr)p);
            //}



            //IntPtr dataResult = IntPtr.Zero;

            //IntPtr result = NTreePackModuleExtern.ExpPackToMemory(dataSource, dataResult);

            int count = 0;
        }


        private NTreePackHandleResult OnExpPackToMemoryCallback(IntPtr dataBuffer)
        {

            return NTreePackHandleResult.Ok;
        }
    }
}
