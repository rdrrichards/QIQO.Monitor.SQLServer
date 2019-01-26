namespace QIQO.Monitor.SQLServer.Data
{
    public class BlockingData : CommonData
    {
        public string LockType { get; set; }
        public string Database { get; set; }
        public int BlockObject { get; set; }
        public string LockRequest { get; set; }
        public int WaiterSid { get; set; }
        public int WaitTime { get; set; }
        public string WaiterBatch { get; set; }
        public string WaiterStatement { get; set; }
        public int BlockerSid { get; set; }
        public string BlockerBatch { get; set; }
    }
}