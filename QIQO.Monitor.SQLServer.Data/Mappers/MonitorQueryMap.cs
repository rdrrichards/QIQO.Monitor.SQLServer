using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class MonitorQueryMap : MapperBase, IMonitorQueryMap
    {
        public MonitorQueryData Map(IDataReader record)
        {
            try
            {
                return new MonitorQueryData()
                {
                    MonitorKey = NullCheck<int>(record["monitor_key"]),
                    QueryKey = NullCheck<int>(record["query_key"]),
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"MonitorQueryMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(MonitorQueryData entity) => new List<SqlParameter>
            {
                BuildParam("@monitor_key", entity.MonitorKey),
                BuildParam("@query_key", entity.QueryKey),
                GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(MonitorQueryData entity) => new List<SqlParameter>
            {
                BuildParam("@monitor_key", entity.MonitorKey),
                BuildParam("@query_key", entity.QueryKey),
                GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(int category_key) => throw new NotImplementedException();
    }
}
