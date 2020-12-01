using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class EnvironmentServerRepository : RepositoryBase<EnvironmentServerData>,
                                     IEnvironmentServerRepository
    {
        private readonly IMonitorDbContext entityContext;
        public EnvironmentServerRepository(IMonitorDbContext dbc, IEnvironmentServerMap map) : base(map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<EnvironmentServerData> GetAll()
        {
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_environment_server_all"));
        }

        public override EnvironmentServerData GetByID(int monitor_key)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_key", monitor_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_environment_server_get", pcol));
        }

        public override void Insert(EnvironmentServerData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(EnvironmentServerData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(EnvironmentServerData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_server_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_server_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(EnvironmentServerData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_environment_server_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
