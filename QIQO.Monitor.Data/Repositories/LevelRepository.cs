using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class LevelRepository : RepositoryBase<LevelData>,
                                     ILevelRepository
    {
        private readonly IMonitorDbContext entityContext;
        public LevelRepository(IMonitorDbContext dbc, ILevelMap map) : base(map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<LevelData> GetAll()
        {
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_level_all"));
        }

        public override LevelData GetByID(int level_key)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@level_key", level_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_level_get", pcol));
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
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_level_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_level_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(LevelData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_level_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
