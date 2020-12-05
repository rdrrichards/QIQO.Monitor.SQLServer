using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class ServiceTypeMap : MapperBase, IServiceTypeMap
    {
        public ServiceTypeData Map(IDataReader record)
        {
            try
            {
                return new ServiceTypeData()
                {
                    ServiceTypeKey = NullCheck<int>(record["ServiceTypeKey"]),
                    ServiceTypeName = NullCheck<string>(record["ServiceTypeName"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"ServiceTypeMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(ServiceTypeData entity) => new List<SqlParameter>
            {
                BuildParam("@ServiceTypeKey", entity.ServiceTypeKey),
                BuildParam("@ServiceTypeName", entity.ServiceTypeName),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(ServiceTypeData entity) => MapParamsForDelete(entity.ServiceTypeKey);

        public List<SqlParameter> MapParamsForDelete(int service_type_key) => new List<SqlParameter>
            {
                BuildParam("@ServiceTypeKey", service_type_key),
                // GetOutParam()
            };
    }
}
