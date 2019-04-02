using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class MonitorTypeMap : MapperBase, IMonitorTypeMap
    {
        public MonitorTypeData Map(IDataReader record)
        {
            try
            {
                return new MonitorTypeData()
                {
                    MonitorTypeKey = NullCheck<int>(record["monitor_type_key"]),
                    MonitorTypeName = NullCheck<string>(record["monitor_type_name"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"MonitorTypeMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(MonitorTypeData entity) => new List<SqlParameter>
            {
                BuildParam("@monitor_type_key", entity.MonitorTypeKey),
                BuildParam("@monitor_type_name", entity.MonitorTypeName),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(MonitorTypeData entity) => MapParamsForDelete(entity.MonitorTypeKey);

        public List<SqlParameter> MapParamsForDelete(int monitor_type_key) => new List<SqlParameter>
            {
                BuildParam("@monitor_type_key", monitor_type_key),
                // GetOutParam()
            };
    }
}
