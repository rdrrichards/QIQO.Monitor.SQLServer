using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;

namespace QIQO.Monitor.Domain
{
    public class WaitStatsResult : MonitorResult<WaitStats>
    {
        public override IEnumerable<WaitStats> results { get; set; } = new List<WaitStats>();
        public override ResultType resultType { get; } = ResultType.WaitStats;
    }
    public partial class WaitStats : IModel
    {
        public WaitStats(long batchNo, string waitType, decimal waitPercentage, decimal avgWaitSec,
            decimal avgResSec, decimal avgSigSec, decimal waitSec, decimal resourceSec,
            decimal signalSec, long waitCount)
        {
            this.batchNo = batchNo;
            this.waitType = waitType;
            this.waitPercentage = waitPercentage;
            this.avgWaitSec = avgWaitSec;
            this.avgResSec = avgResSec;
            this.avgSigSec = avgSigSec;
            this.waitSec = waitSec;
            this.resourceSec = resourceSec;
            this.signalSec = signalSec;
            this.waitCount = waitCount;
        }
        public long batchNo { get; }
        public string waitType { get; }
        public decimal waitPercentage { get; }
        public decimal avgWaitSec { get; }
        public decimal avgResSec { get; }
        public decimal avgSigSec { get; }
        public decimal waitSec { get; }
        public decimal resourceSec { get; }
        public decimal signalSec { get; }
        public long waitCount { get; }
    }
}
