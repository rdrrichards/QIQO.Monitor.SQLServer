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
                    ServiceKey = NullCheck<int>(record["ServiceKey"]),
                    MonitorKey = NullCheck<int>(record["MonitorKey"]),
                    AttributeTypeKey = NullCheck<int>(record["AttributeTypeKey"]),
                    AttributeValue = NullCheck<string>(record["AttributeValue"]),
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"EnvironmentServiceMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(ServiceMonitorAttributeData entity) => new List<SqlParameter>
            {
                BuildParam("@ServiceKey", entity.ServiceKey),
                BuildParam("@MonitorKey", entity.MonitorKey),
                BuildParam("@AttributeTypeKey", entity.AttributeTypeKey),
                BuildParam("@AttributeValue", entity.AttributeValue),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(ServiceMonitorAttributeData entity) => new List<SqlParameter>
            {
                BuildParam("@ServiceKey", entity.ServiceKey),
                BuildParam("@MonitorKey", entity.MonitorKey),
                BuildParam("@AttributeTypeKey", entity.AttributeTypeKey),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(int key) => throw new NotImplementedException();
    }
}
