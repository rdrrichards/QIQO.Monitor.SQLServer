using QIQO.Monitor.Core;
using System;
using System.Data;

namespace QIQO.Monitor.SQLServer.Data
{
    public class WaitStatsMap : MapperBase, IWaitStatsMap
    {
        public WaitStatsData Map(IDataReader record)
        {
            try
            {
                return new WaitStatsData()
                {
                    BatchNo = NullCheck<long>(record["BatchNo"]),
                    WaitType = NullCheck<string>(record["WaitType"]),
                    WaitTypeKey = NullCheck<long>(record["WaitTypeKey"]),
                    WaitPercentage = NullCheck<decimal>(record["WaitPercentage"]),
                    AvgWaitSec = NullCheck<decimal>(record["AvgWaitSec"]),
                    AvgResSec = NullCheck<decimal>(record["AvgResSec"]),
                    AvgSigSec = NullCheck<decimal>(record["AvgSigSec"]),
                    WaitSec = NullCheck<decimal>(record["WaitSec"]),
                    ResourceSec = NullCheck<decimal>(record["ResourceSec"]),
                    SignalSec = NullCheck<decimal>(record["SignalSec"]),
                    WaitCount = NullCheck<long>(record["WaitCount"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"WaitStatsMap Exception occured: {ex.Message}", ex);
            }
        }
    }
}
