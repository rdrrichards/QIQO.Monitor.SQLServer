using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class EnvironmentMap : MapperBase, IEnvironmentMap
    {
        public EnvironmentData Map(IDataReader record)
        {
            try
            {
                return new EnvironmentData()
                {
                    EnvironmentKey = NullCheck<int>(record["environment_key"]),
                    EnvironmentName = NullCheck<string>(record["environment_name"]),
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"EnvironmentMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(EnvironmentData entity) => new List<SqlParameter>
            {
                BuildParam("@environment_key", entity.EnvironmentKey),
                BuildParam("@environment_name", entity.EnvironmentName),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(EnvironmentData entity) => new List<SqlParameter>
            {
                BuildParam("@environment_key", entity.EnvironmentKey),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(int category_key) => throw new NotImplementedException();
    }
}
