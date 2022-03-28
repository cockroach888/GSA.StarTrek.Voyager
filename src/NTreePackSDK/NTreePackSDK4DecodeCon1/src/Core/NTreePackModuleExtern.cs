using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NTreePackSDK4DecodeCon1.Core
{
    internal static class NTreePackModuleExtern
    {
        [DllImport("NTreePackModule", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr ExpCreatePackFile(string filePath, uint fileFlg = 1);


        [DllImport("NTreePackModule")]
        public static extern bool ExpGetWordB(IntPtr dataSourceBuffer, string fieldName, IntPtr dataResult);


        [DllImport("NTreePackModule", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern uint ExpGetChildCount(IntPtr dataSourceBuffer);


        [DllImport("NTreePackModule", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr ExpPackToMemory(IntPtr dataSourceBuffer, IntPtr dataResultBuffer, uint length = 0);
    }





    internal static class NTreePackModuleExternTest
    {
        [DllImport("NTreePackModule")]
        public static extern bool ExpGetWordB(IntPtr dataSourceBuffer, string fieldName, IntPtr dataResult);


        [DllImport("NTreePackModule")]
        public static extern bool ExpGetWordB(IntPtr dataSourceBuffer, string fieldName, ref ushort dataResult);


        [DllImport("NTreePackModule")]
        public unsafe static extern bool ExpGetWordB(IntPtr dataSourceBuffer, string fieldName, ushort* dataResult);
    }


    internal static class NTreePackModuleExternTestExtension
    {

        public static bool ExpGetWordB(IntPtr dataSourceBuffer, string fieldName, out ushort dataResult)
        {
            dataResult = 999;
            unsafe
            {
                fixed (ushort* data = &dataResult)
                {
                    return NTreePackModuleExternTest.ExpGetWordB(dataSourceBuffer, fieldName, data);
                }
            }
        }
    }




    public class TestA
    {
        public void Call()
        {
            IntPtr filePointer = IntPtr.Zero;

            ushort result = 999;

            // IntPtr 调用
            unsafe
            {
                ushort* resultPointer = &result;

                NTreePackModuleExternTest.ExpGetWordB(filePointer, "Id", (IntPtr)resultPointer);
            }
            Trace.TraceInformation($"Id: {result}");


            // 引用传递
            NTreePackModuleExternTest.ExpGetWordB(filePointer, "Id", ref result);
            Trace.TraceInformation($"Id: {result}");


            // 不安全包装调用
            NTreePackModuleExternTestExtension.ExpGetWordB(filePointer, "Id", out ushort dataResult);
            Trace.TraceInformation($"Id: {dataResult}");
        }
    }

}
