﻿using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class EnvironmentServerMap : MapperBase, IEnvironmentServerMap
    {
        public EnvironmentServerData Map(IDataReader record)
        {
            try
            {
                return new EnvironmentServerData()
                {
                    EnvironmentKey = NullCheck<int>(record["environment_key"]),
                    ServerKey = NullCheck<int>(record["server_key"]),
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"EnvironmentServerMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(EnvironmentServerData entity) => new List<SqlParameter>
            {
                BuildParam("@environment_key", entity.EnvironmentKey),
                BuildParam("@server_key", entity.ServerKey),
                GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(EnvironmentServerData entity) => new List<SqlParameter>
            {
                BuildParam("@environment_key", entity.EnvironmentKey),
                BuildParam("@server_key", entity.ServerKey),
                GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(int category_key) => throw new NotImplementedException();
    }
}
