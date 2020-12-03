using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;

namespace QIQO.Monitor.Domain
{
    public class BlockingResult : MonitorResult<Blocking>
    {
        public override ResultType resultType => ResultType.Blocking;
        public override IEnumerable<Blocking> results { get; set; } = new List<Blocking>();
    }
    public partial class Blocking : IModel
    {
        public Blocking(string lockType, string database, long blockObject,
            string lockRequest, int waiterSid, long waitTime, string waiterBatch,
            string waiterStatement, int blockerSid, string blockerBatch)
        {
            this.lockType = lockType;
            this.database = database;
            this.blockObject = blockObject;
            this.lockRequest = lockRequest;
            this.waiterSid = waiterSid;
            this.waitTime = waitTime;
            this.waiterBatch = waiterBatch;
            this.waiterStatement = waiterStatement;
            this.blockerSid = blockerSid;
            this.blockerBatch = blockerBatch;
        }
        public string lockType { get; }
        public string database { get; }
        public long blockObject { get; }
        public string lockRequest { get; }
        public int waiterSid { get; }
        public long waitTime { get; }
        public string waiterBatch { get; }
        public string waiterStatement { get; }
        public int blockerSid { get; }
        public string blockerBatch { get; }
    }
}
