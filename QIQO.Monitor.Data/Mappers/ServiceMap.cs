using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class ServiceMap : MapperBase, IServiceMap
    {
        public ServiceData Map(IDataReader record)
        {
            try
            {
                return new ServiceData()
                {
                    ServiceKey = NullCheck<int>(record["ServiceKey"]),
                    ServerKey = NullCheck<int>(record["ServerKey"]),
                    ServiceTypeKey = NullCheck<int>(record["ServiceTypeKey"]),
                    ServiceName = NullCheck<string>(record["ServiceName"]),
                    InstanceName = NullCheck<string>(record["InstanceName"]),
                    ServiceSource = NullCheck<string>(record["ServiceSource"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"ServiceMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(ServiceData entity) => new List<SqlParameter>
            {
                BuildParam("@ServiceKey", entity.ServiceKey),
                BuildParam("@ServerKey", entity.ServerKey),
                BuildParam("@ServiceTypeKey", entity.ServiceTypeKey),
                BuildParam("@ServiceName", entity.ServiceName),
                BuildParam("@InstanceName", entity.InstanceName),
                BuildParam("@ServiceSource", entity.ServiceSource),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(ServiceData entity) => MapParamsForDelete(entity.ServiceKey);

        public List<SqlParameter> MapParamsForDelete(int category_key) => new List<SqlParameter>
            {
                BuildParam("@ServiceKey", category_key),
                // GetOutParam()
            };
    }
}
