using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;

namespace QIQO.Monitor.Domain
{
    public class BlockingResult : MonitorResult<Blocking>
    {
        public override List<Blocking> Results { get; } = new List<Blocking>();
    }
    public partial class Blocking : IModel
    {
        public Blocking(string lockType, string database, long blockObject,
            string lockRequest, int waiterSid, long waitTime, string waiterBatch,
            string waiterStatement, int blockerSid, string blockerBatch)
        {
            LockType = lockType;
            Database = database;
            BlockObject = blockObject;
            LockRequest = lockRequest;
            WaiterSid = waiterSid;
            WaitTime = waitTime;
            WaiterBatch = waiterBatch;
            WaiterStatement = waiterStatement;
            BlockerSid = blockerSid;
            BlockerBatch = blockerBatch;
        }
        public string LockType { get; }
        public string Database { get; }
        public long BlockObject { get; }
        public string LockRequest { get; }
        public int WaiterSid { get; }
        public long WaitTime { get; }
        public string WaiterBatch { get; }
        public string WaiterStatement { get; }
        public int BlockerSid { get; }
        public string BlockerBatch { get; }
    }
}
