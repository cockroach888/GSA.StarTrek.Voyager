namespace SuperSocket4ClientConsole1
{
    internal enum OpCode : byte
    {
        Connect = 1,
        Subscribe = 2,
        Publish = 3
    }

    internal class MyPackage
    {
        public OpCode Code { get; set; }

        public short Sequence { get; set; }

        public string Body { get; set; }
    }
}
