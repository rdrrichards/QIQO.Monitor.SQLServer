using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class EnvironmentServiceMap : MapperBase, IEnvironmentServiceMap
    {
        public EnvironmentServiceData Map(IDataReader record)
        {
            try
            {
                return new EnvironmentServiceData()
                {
                    EnvironmentKey = NullCheck<int>(record["environment_key"]),
                    ServiceKey = NullCheck<int>(record["service_key"]),
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"EnvironmentServiceMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(EnvironmentServiceData entity) => new List<SqlParameter>
            {
                BuildParam("@environment_key", entity.EnvironmentKey),
                BuildParam("@service_key", entity.ServiceKey),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(EnvironmentServiceData entity) => new List<SqlParameter>
            {
                BuildParam("@environment_key", entity.EnvironmentKey),
                BuildParam("@service_key", entity.ServiceKey),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(int category_key) => throw new NotImplementedException();
    }
}
