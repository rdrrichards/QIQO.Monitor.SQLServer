using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class MonitorRepository : RepositoryBase<MonitorData>,
                                     IMonitorRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public MonitorRepository(IMonitorDbContext dbc, IMonitorMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<MonitorData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitorGetAll"));
        }

        public override MonitorData GetByID(int monitorKey)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitorKey", monitorKey) };
            using (_entityContext) return MapRow(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitorGet", pcol));
        }

        public override void Insert(MonitorData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(MonitorData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(MonitorData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorDelete", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorDelete", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(MonitorData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }
}
