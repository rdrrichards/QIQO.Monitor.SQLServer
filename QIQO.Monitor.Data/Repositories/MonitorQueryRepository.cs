using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class MonitorQueryRepository : RepositoryBase<MonitorQueryData>,
                                     IMonitorQueryRepository
    {
        private readonly IMonitorDbContext entityContext;
        public MonitorQueryRepository(IMonitorDbContext dbc, IMonitorQueryMap map) : base(map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<MonitorQueryData> GetAll()
        {
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_monitor_query_all"));
        }

        public override MonitorQueryData GetByID(int monitor_key)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_key", monitor_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_monitor_query_get", pcol));
        }

        public override void Insert(MonitorQueryData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(MonitorQueryData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(MonitorQueryData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_query_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_query_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(MonitorQueryData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_query_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
