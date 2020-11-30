using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class ServerMap : MapperBase, IServerMap
    {
        public ServerData Map(IDataReader record)
        {
            try
            {
                return new ServerData()
                {
                    ServerKey = NullCheck<int>(record["server_key"]),
                    ServerName = NullCheck<string>(record["server_name"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"ServerMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(ServerData entity) => new List<SqlParameter>
            {
                BuildParam("@server_key", entity.ServerKey),
                BuildParam("@server_name", entity.ServerName),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(ServerData entity) => MapParamsForDelete(entity.ServerKey);

        public List<SqlParameter> MapParamsForDelete(int server_key) => new List<SqlParameter>
            {
                BuildParam("@server_key", server_key),
                // GetOutParam()
            };
    }
}
