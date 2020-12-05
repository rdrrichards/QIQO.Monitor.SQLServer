using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class LevelRepository : RepositoryBase<LevelData>,
                                     ILevelRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public LevelRepository(IMonitorDbContext dbc, ILevelMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<LevelData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitorLevelGetAll"));
        }

        public override LevelData GetByID(int monitorLevelKey)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@MonitorLevelKey", monitorLevelKey) };
            using (_entityContext) return MapRow(_entityContext.ExecuteProcedureAsSqlDataReader("monMonitorLevelGet", pcol));
        }

        public override void Insert(LevelData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(LevelData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(LevelData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorLevelDelete", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorLevelDelete", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(LevelData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monMonitorLevelUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }
}
