using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class EnvironmentServerRepository : RepositoryBase<EnvironmentServerData>,
                                     IEnvironmentServerRepository
    {
        private readonly IMonitorDbContext entityContext;
        public EnvironmentServerRepository(IMonitorDbContext dbc, IEnvironmentServerMap map, ILogger<EnvironmentServerData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<EnvironmentServerData> GetAll()
        {
            Log.LogInformation("Accessing EnvironmentServerRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_environment_server_all"));
        }

        public override EnvironmentServerData GetByID(int monitor_key)
        {
            Log.LogInformation("Accessing EnvironmentServerRepository GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_key", monitor_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_environment_server_get", pcol));
        }

        public override EnvironmentServerData GetByCode(string monitor_code, string entityCode)
        {
            Log.LogInformation("Accessing EnvironmentServerRepository GetByCode function");
            var pcol = new List<SqlParameter>() {
                Mapper.BuildParam("@monitor_code", monitor_code),
                Mapper.BuildParam("@company_code", entityCode)
            };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_environment_server_get_c", pcol));
        }

        public override void Insert(EnvironmentServerData entity)
        {
            Log.LogInformation("Accessing EnvironmentServerRepository Insert function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(EnvironmentServerData entity)
        {
            Log.LogInformation("Accessing EnvironmentServerRepository Save function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(EnvironmentServerData entity)
        {
            Log.LogInformation("Accessing EnvironmentServerRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_server_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByCode(string entityCode)
        {
            Log.LogInformation("Accessing EnvironmentServerRepository DeleteByCode function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_code", entityCode) };
            pcol.Add(Mapper.GetOutParam());
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_server_del_c", pcol);
        }

        public override void DeleteByID(int entityKey)
        {
            Log.LogInformation("Accessing EnvironmentServerRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_server_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(EnvironmentServerData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_server_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
