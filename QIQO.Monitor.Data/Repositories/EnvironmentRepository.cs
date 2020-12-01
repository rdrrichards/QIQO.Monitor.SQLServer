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
        public EnvironmentRepository(IMonitorDbContext dbc, IEnvironmentMap map) : base(map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<EnvironmentData> GetAll()
        {
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_environment_all"));
        }

        public override EnvironmentData GetByID(int monitor_key)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_key", monitor_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_environment_get", pcol));
        }

        public override void Insert(EnvironmentData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(EnvironmentData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(EnvironmentData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(EnvironmentData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
