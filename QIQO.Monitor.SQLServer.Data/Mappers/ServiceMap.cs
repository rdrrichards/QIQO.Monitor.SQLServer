using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class ServiceMap : MapperBase, IServiceMap
    {
        public ServiceData Map(IDataReader record)
        {
            try
            {
                return new ServiceData()
                {
                    ServiceKey = NullCheck<int>(record["service_key"]),
                    ServerKey = NullCheck<int>(record["server_key"]),
                    ServiceTypeKey = NullCheck<int>(record["service_type_key"]),
                    ServiceName = NullCheck<string>(record["service_name"]),
                    InstanceName = NullCheck<string>(record["instance_name"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"ServiceMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(ServiceData entity) => new List<SqlParameter>
            {
                new SqlParameter("@service_key", entity.ServiceKey),
                new SqlParameter("@server_key", entity.ServerKey),
                new SqlParameter("@service_type_key", entity.ServiceTypeKey),
                new SqlParameter("@service_name", entity.ServiceName),
                new SqlParameter("@instance_name", entity.InstanceName),
                GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(ServiceData entity) => MapParamsForDelete(entity.ServiceKey);

        public List<SqlParameter> MapParamsForDelete(int category_key) => new List<SqlParameter>
            {
                new SqlParameter("@service_key", category_key),
                GetOutParam()
            };
    }
}
