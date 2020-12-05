using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class MonitorTypeRepository : RepositoryBase<MonitorTypeData>,
                                     IMonitorTypeRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public MonitorTypeRepository(IMonitorDbContext dbc, IMonitorTypeMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<MonitorTypeData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitorTypeGetAll"));
        }

        public override MonitorTypeData GetByID(int monitorTypeKey)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@monitorTypeKey", monitorTypeKey) };
            using (_entityContext) return MapRow(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitorTypeGet", pcol));
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
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorTypeDelete", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorTypeDelete", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(MonitorTypeData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorTypeUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }

}
