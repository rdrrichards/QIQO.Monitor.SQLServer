using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class ServiceMonitorMap : MapperBase, IServiceMonitorMap
    {
        public ServiceMonitorData Map(IDataReader record)
        {
            try
            {
                return new ServiceMonitorData()
                {
                    ServiceKey = NullCheck<int>(record["service_key"]),
                    MonitorKey = NullCheck<int>(record["monitor_key"]),
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"EnvironmentServiceMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(ServiceMonitorData entity) => new List<SqlParameter>
            {
                BuildParam("@service_key", entity.ServiceKey),
                BuildParam("@monitor_key", entity.MonitorKey),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(ServiceMonitorData entity) => new List<SqlParameter>
            {
                BuildParam("@service_key", entity.ServiceKey),
                BuildParam("@monitor_key", entity.MonitorKey),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(int category_key) => throw new NotImplementedException();
    }
}
