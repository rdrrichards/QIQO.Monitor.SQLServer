using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;

namespace QIQO.Monitor.Domain
{
    public class WaitStatsResult : MonitorResult<WaitStats>
    {
        public override List<WaitStats> Results { get; } = new List<WaitStats>();
    }
    public partial class WaitStats : IModel
    {
        public WaitStats(long batchNo, string waitType, decimal waitPercentage, decimal avgWaitSec,
            decimal avgResSec, decimal avgSigSec, decimal waitSec, decimal resourceSec,
            decimal signalSec, long waitCount)
        {
            BatchNo = batchNo;
            WaitType = waitType;
            WaitPercentage = waitPercentage;
            AvgWaitSec = avgWaitSec;
            AvgResSec = avgResSec;
            AvgSigSec = avgSigSec;
            WaitSec = waitSec;
            ResourceSec = resourceSec;
            SignalSec = signalSec;
            WaitCount = waitCount;
        }
        public long BatchNo { get; }
        public string WaitType { get; }
        public decimal WaitPercentage { get; }
        public decimal AvgWaitSec { get; }
        public decimal AvgResSec { get; }
        public decimal AvgSigSec { get;}
        public decimal WaitSec { get; }
        public decimal ResourceSec { get; }
        public decimal SignalSec { get; }
        public long WaitCount { get; }
    }
}
