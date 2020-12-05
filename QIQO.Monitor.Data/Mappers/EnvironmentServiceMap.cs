using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class EnvironmentServiceMap : MapperBase, IEnvironmentServiceMap
    {
        public EnvironmentServiceData Map(IDataReader record)
        {
            try
            {
                return new EnvironmentServiceData()
                {
                    EnvironmentKey = NullCheck<int>(record["EnvironmentKey"]),
                    ServiceKey = NullCheck<int>(record["ServiceKey"]),
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"EnvironmentServiceMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(EnvironmentServiceData entity) => new List<SqlParameter>
            {
                BuildParam("@EnvironmentKey", entity.EnvironmentKey),
                BuildParam("@ServiceKey", entity.ServiceKey),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(EnvironmentServiceData entity) => new List<SqlParameter>
            {
                BuildParam("@EnvironmentKey", entity.EnvironmentKey),
                BuildParam("@ServiceKey", entity.ServiceKey),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(int category_key) => throw new NotImplementedException();
    }
}
