using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class QueryRepository : RepositoryBase<QueryData>,
                                     IQueryRepository
    {
        private readonly IMonitorDbContext _entityContext;
        public QueryRepository(IMonitorDbContext dbc, IQueryMap map) : base(map)
        {
            _entityContext = dbc;
        }

        public override IEnumerable<QueryData> GetAll()
        {
            using (_entityContext) return MapRows(_entityContext.ExecuteProcedureAsSqlDataReader("monQueryGetAll"));
        }

        public override QueryData GetByID(int queryKey)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@queryKey", queryKey) };
            using (_entityContext) return MapRow(_entityContext.ExecuteProcedureAsSqlDataReader("monQueryGet", pcol));
        }

        public override void Insert(QueryData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(QueryData entity)
        {
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(QueryData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monQueryDelete", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monQueryDelete", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(QueryData entity)
        {
            using (_entityContext) _entityContext.ExecuteProcedureNonQuery("monQueryUpsert", Mapper.MapParamsForUpsert(entity));
        }
    }
}
