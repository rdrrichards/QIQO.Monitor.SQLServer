using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class QueryHistoryRepository : RepositoryBase<QueryHistoryData>,
                                     IQueryHistoryRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public QueryHistoryRepository(IMonitorDbContext dbc, IQueryHistoryMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<QueryHistoryData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monQueryHistoryGetAll"));
        }

        public override QueryHistoryData GetByID(int queryHistoryKey)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@queryHistoryKey", queryHistoryKey) };
            using (_entityContext) return MapRow(_entityContext.ExecuteProcedureAsSqlDataReader("monQueryHistoryGet", pcol));
        }

        public override void Insert(QueryHistoryData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(QueryHistoryData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(QueryHistoryData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monQueryHistoryDelete", Mapper.MapParamsForDelete(entity));
        }
        public override void DeleteByID(int entityKey)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monQueryHistoryDelete", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(QueryHistoryData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monQueryHistoryUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }
}
