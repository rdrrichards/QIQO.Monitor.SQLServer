﻿using QIQO.Monitor.Core;
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
                    ServerKey = NullCheck<int>(record["ServerKey"]),
                    ServerName = NullCheck<string>(record["ServerName"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"ServerMap Exception occured: {ex.Message}", ex);
            }
        }

        public List<SqlParameter> MapParamsForUpsert(ServerData entity) => new List<SqlParameter>
            {
                BuildParam("@ServerKey", entity.ServerKey),
                BuildParam("@ServerName", entity.ServerName),
                // GetOutParam()
            };

        public List<SqlParameter> MapParamsForDelete(ServerData entity) => MapParamsForDelete(entity.ServerKey);

        public List<SqlParameter> MapParamsForDelete(int server_key) => new List<SqlParameter>
            {
                BuildParam("@ServerKey", server_key),
                // GetOutParam()
            };
    }
}
