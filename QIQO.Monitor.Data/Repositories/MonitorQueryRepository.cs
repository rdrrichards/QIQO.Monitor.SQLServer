using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class MonitorQueryRepository : RepositoryBase<MonitorQueryData>,
                                     IMonitorQueryRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public MonitorQueryRepository(IMonitorDbContext dbc, IMonitorQueryMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<MonitorQueryData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitorQueryGetAll"));
        }

        public override MonitorQueryData GetByID(int monitor_key)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitor_key", monitor_key) };
            using (_entityContext) return MapRow(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitorQueryGet", pcol));
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
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorQueryDelete", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorQueryDelete", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(MonitorQueryData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorQueryUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }
}
