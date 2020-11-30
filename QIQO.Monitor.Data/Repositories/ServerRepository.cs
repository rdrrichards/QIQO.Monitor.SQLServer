using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class ServerRepository : RepositoryBase<ServerData>,
                                     IServerRepository
    {
        private readonly IMonitorDbContext entityContext;
        public ServerRepository(IMonitorDbContext dbc, IServerMap map, ILogger<ServerData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<ServerData> GetAll()
        {
            Log.LogInformation("Accessing ServerRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_server_all"));
        }

        public override ServerData GetByID(int server_key)
        {
            Log.LogInformation("Accessing ServerRepository GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@server_key", server_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_server_get", pcol));
        }

        public override void Insert(ServerData entity)
        {
            Log.LogInformation("Accessing ServerRepository Insert function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(ServerData entity)
        {
            Log.LogInformation("Accessing ServerRepository Save function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(ServerData entity)
        {
            Log.LogInformation("Accessing ServerRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_server_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            Log.LogInformation("Accessing ServerRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_server_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(ServerData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_server_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
