using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class EnvironmentServiceRepository : RepositoryBase<EnvironmentServiceData>,
                                     IEnvironmentServiceRepository
    {
        private readonly IMonitorDbContext entityContext;
        public EnvironmentServiceRepository(IMonitorDbContext dbc, IEnvironmentServiceMap map, ILogger<EnvironmentServiceData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<EnvironmentServiceData> GetAll()
        {
            Log.LogInformation("Accessing EnvironmentServiceRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_environment_service_all"));
        }

        public override EnvironmentServiceData GetByID(int monitor_key)
        {
            Log.LogInformation("Accessing EnvironmentServiceRepository GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_key", monitor_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_environment_service_get", pcol));
        }

        public override void Insert(EnvironmentServiceData entity)
        {
            Log.LogInformation("Accessing EnvironmentServiceRepository Insert function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(EnvironmentServiceData entity)
        {
            Log.LogInformation("Accessing EnvironmentServiceRepository Save function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(EnvironmentServiceData entity)
        {
            Log.LogInformation("Accessing EnvironmentServiceRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_service_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            Log.LogInformation("Accessing EnvironmentServiceRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_service_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(EnvironmentServiceData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_service_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
