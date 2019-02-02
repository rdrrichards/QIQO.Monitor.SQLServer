using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class MonitorMap : MapperBase, IMonitorMap
    {
        public MonitorData Map(IDataReader record)
        {
            try
            {
                return new MonitorData()
                {
                    MonitorKey = NullCheck<int>(record["monitor_key"]),
                    MonitorTypeKey = NullCheck<int>(record["monitor_type_key"]),
                    MonitorName = NullCheck<string>(record["monitor_name"]),
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"MonitorMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(MonitorData entity) => new List<SqlParameter>
            {
                new SqlParameter("@monitor_key", entity.MonitorKey),
                new SqlParameter("@monitor_type_key", entity.MonitorTypeKey),
                new SqlParameter("@monitor_name", entity.MonitorName),
                GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(MonitorData entity) => MapParamsForDelete(entity.MonitorKey);

        public List<SqlParameter> MapParamsForDelete(int category_key) => new List<SqlParameter>
            {
                new SqlParameter("@monitor_key", category_key),
                GetOutParam()
            };
    }
}
