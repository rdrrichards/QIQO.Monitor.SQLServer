using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class WaitStatsLogMap : MapperBase, IWaitStatsLogMap
    {
        public WaitStatsLogData Map(IDataReader record)
        {
            try
            {
                return new WaitStatsLogData()
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
                    WaitCount = NullCheck<long>(record["WaitCount"]),
                    ServiceKey = NullCheck<int>(record["ServiceKey"]),
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"WaitStatsLogMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForDelete(WaitStatsLogData entity) => throw new NotImplementedException();

        public List<SqlParameter> MapParamsForDelete(int entity_key) => throw new NotImplementedException();

        public List<SqlParameter> MapParamsForUpsert(WaitStatsLogData entity) => new List<SqlParameter>
            {
                BuildParam("@BatchNo", entity.BatchNo),
                BuildParam("@WaitType", entity.WaitType),
                BuildParam("@WaitPercentage", entity.WaitPercentage),
                BuildParam("@AvgWaitSec", entity.AvgWaitSec),
                BuildParam("@AvgResSec", entity.AvgResSec),
                BuildParam("@AvgSigSec", entity.AvgSigSec),
                BuildParam("@WaitSec", entity.WaitSec),
                BuildParam("@ResourceSec", entity.ResourceSec),
                BuildParam("@SignalSec", entity.SignalSec),
                BuildParam("@WaitCount", entity.WaitCount),
                BuildParam("@ServiceKey", entity.ServiceKey)
            };
    }
}
