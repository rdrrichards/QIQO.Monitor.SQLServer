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
        public EnvironmentServiceRepository(IMonitorDbContext dbc, IEnvironmentServiceMap map) : base(map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<EnvironmentServiceData> GetAll()
        {
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_environment_service_all"));
        }

        public override EnvironmentServiceData GetByID(int monitor_key)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_key", monitor_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_environment_service_get", pcol));
        }

        public override void Insert(EnvironmentServiceData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(EnvironmentServiceData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(EnvironmentServiceData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_service_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_service_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(EnvironmentServiceData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_service_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
