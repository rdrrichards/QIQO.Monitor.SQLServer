using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class MonitorTypeRepository : RepositoryBase<MonitorTypeData>,
                                     IMonitorTypeRepository
    {
        private readonly IMonitorDbContext entityContext;
        public MonitorTypeRepository(IMonitorDbContext dbc, IMonitorTypeMap map) : base(map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<MonitorTypeData> GetAll()
        {
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_monitor_type_all"));
        }

        public override MonitorTypeData GetByID(int monitor_type_key)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_type_key", monitor_type_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_monitor_type_get", pcol));
        }

        public override void Insert(MonitorTypeData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(MonitorTypeData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(MonitorTypeData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_type_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_type_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(MonitorTypeData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_monitor_type_ups", Mapper.MapParamsForUpsert(entity));
        }
    }

}
