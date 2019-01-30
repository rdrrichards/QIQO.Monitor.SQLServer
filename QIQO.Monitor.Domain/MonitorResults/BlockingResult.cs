using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;

namespace QIQO.Monitor.Domain
{
    public class BlockingResult : MonitorResult, IResultPayload<Blocking>
    {
        IEnumerable<IModel> IResultPayload.Results => new List<Blocking>();
    }
    public partial class Blocking : IModel
    {
        public Blocking(string lockType, string database, int blockObject,
            string lockRequest, int waiterSid, int waitTime, string waiterBatch,
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
        public int BlockObject { get; }
        public string LockRequest { get; }
        public int WaiterSid { get; }
        public int WaitTime { get; }
        public string WaiterBatch { get; }
        public string WaiterStatement { get; }
        public int BlockerSid { get; }
        public string BlockerBatch { get; }
    }
}
