using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class EnvironmentRepository : RepositoryBase<EnvironmentData>,
                                     IEnvironmentRepository
    {
        private readonly IMonitorDbContext entityContext;
        public EnvironmentRepository(IMonitorDbContext dbc, IEnvironmentMap map, ILogger<EnvironmentData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<EnvironmentData> GetAll()
        {
            Log.LogInformation("Accessing EnvironmentRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_environment_all"));
        }

        public override EnvironmentData GetByID(int monitor_key)
        {
            Log.LogInformation("Accessing EnvironmentRepository GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_key", monitor_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_environment_get", pcol));
        }

        public override void Insert(EnvironmentData entity)
        {
            Log.LogInformation("Accessing EnvironmentRepository Insert function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(EnvironmentData entity)
        {
            Log.LogInformation("Accessing EnvironmentRepository Save function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(EnvironmentData entity)
        {
            Log.LogInformation("Accessing EnvironmentRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            Log.LogInformation("Accessing EnvironmentRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(EnvironmentData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
