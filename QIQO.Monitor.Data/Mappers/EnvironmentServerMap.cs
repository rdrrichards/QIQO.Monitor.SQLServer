using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class EnvironmentServerMap : MapperBase, IEnvironmentServerMap
    {
        public EnvironmentServerData Map(IDataReader record)
        {
            try
            {
                return new EnvironmentServerData()
                {
                    EnvironmentKey = NullCheck<int>(record["EnvironmentKey"]),
                    ServerKey = NullCheck<int>(record["ServerKey"]),
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"EnvironmentServerMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(EnvironmentServerData entity) => new List<SqlParameter>
            {
                BuildParam("@EnvironmentKey", entity.EnvironmentKey),
                BuildParam("@ServerKey", entity.ServerKey),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(EnvironmentServerData entity) => new List<SqlParameter>
            {
                BuildParam("@EnvironmentKey", entity.EnvironmentKey),
                BuildParam("@ServerKey", entity.ServerKey),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(int category_key) => throw new NotImplementedException();
    }
}
