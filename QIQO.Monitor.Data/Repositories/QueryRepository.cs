using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Data
{
    public class QueryRepository : RepositoryBase<QueryData>,
                                     IQueryRepository
    {
        private readonly IMonitorDbContext entityContext;
        public QueryRepository(IMonitorDbContext dbc, IQueryMap map) : base(map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<QueryData> GetAll()
        {
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_query_all"));
        }

        public override QueryData GetByID(int query_key)
        {
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@query_key", query_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_query_get", pcol));
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
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_query_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByID(int entityKey)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_query_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(QueryData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_query_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
