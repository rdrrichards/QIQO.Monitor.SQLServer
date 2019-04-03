using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.Domain;
using QIQO.Monitor.SQLServer.Data;
using System;

namespace QIQO.Monitor.Api.Services
{
    public interface IWaitStatsEntityService : IEntityService<WaitStats, WaitStatsData> { }
    public class WaitStatsEntityService : IWaitStatsEntityService
    {
        public WaitStats Map(WaitStatsData ent) => new WaitStats(ent.BatchNo, ent.WaitType, ent.WaitPercentage, ent.AvgWaitSec,
                ent.AvgResSec, ent.AvgSigSec, ent.WaitSec, ent.ResourceSec,
                ent.SignalSec, ent.WaitCount);

        public WaitStatsData Map(WaitStats ent) => throw new NotImplementedException();
    }
}
