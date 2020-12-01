using QIQO.Monitor.Core.Contracts;

namespace QIQO.Monitor.SQLServer.Data
{
    public class BlockingData : IEntity
    {
        public string LockType { get; set; }
        public string Database { get; set; }
        public long BlockObject { get; set; }
        public string LockRequest { get; set; }
        public int WaiterSid { get; set; }
        public long WaitTime { get; set; }
        public string WaiterBatch { get; set; }
        public string WaiterStatement { get; set; }
        public short BlockerSid { get; set; }
        public string BlockerBatch { get; set; }
    }
}