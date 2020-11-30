using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class ServiceMonitorAttributeMap : MapperBase, IServiceMonitorAttributeMap
    {
        public ServiceMonitorAttributeData Map(IDataReader record)
        {
            try
            {
                return new ServiceMonitorAttributeData()
                {
                    ServiceKey = NullCheck<int>(record["service_key"]),
                    MonitorKey = NullCheck<int>(record["monitor_key"]),
                    AttributeTypeKey = NullCheck<int>(record["attribute_type_key"]),
                    AttributeValue = NullCheck<string>(record["attribute_value"]),
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"EnvironmentServiceMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(ServiceMonitorAttributeData entity) => new List<SqlParameter>
            {
                BuildParam("@service_key", entity.ServiceKey),
                BuildParam("@monitor_key", entity.MonitorKey),
                BuildParam("@attribute_type_key", entity.AttributeTypeKey),
                BuildParam("@attribute_value", entity.AttributeValue),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(ServiceMonitorAttributeData entity) => new List<SqlParameter>
            {
                BuildParam("@service_key", entity.ServiceKey),
                BuildParam("@monitor_key", entity.MonitorKey),
                BuildParam("@attribute_type_key", entity.AttributeTypeKey),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(int key) => throw new NotImplementedException();
    }
}
