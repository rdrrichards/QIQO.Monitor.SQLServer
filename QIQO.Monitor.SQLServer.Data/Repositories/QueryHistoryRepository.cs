using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class QueryHistoryRepository : RepositoryBase<QueryHistoryData>,
                                     IQueryHistoryRepository
    {
        private readonly IMonitorDbContext entityContext;
        public QueryHistoryRepository(IMonitorDbContext dbc, IQueryHistoryMap map, ILogger<QueryHistoryData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<QueryHistoryData> GetAll()
        {
            Log.LogInformation("Accessing QueryHistoryRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_query_history_all"));
        }

        public override QueryHistoryData GetByID(int query_history_key)
        {
            Log.LogInformation("Accessing QueryHistoryRepository GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@query_history_key", query_history_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_query_history_get", pcol));
        }

        public override QueryHistoryData GetByCode(string query_history_code, string entityCode)
        {
            Log.LogInformation("Accessing QueryHistoryRepository GetByCode function");
            var pcol = new List<SqlParameter>() {
                Mapper.BuildParam("@query_history_code", query_history_code),
                Mapper.BuildParam("@company_code", entityCode)
            };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_query_history_get_c", pcol));
        }

        public override void Insert(QueryHistoryData entity)
        {
            Log.LogInformation("Accessing QueryHistoryRepository Insert function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(QueryHistoryData entity)
        {
            Log.LogInformation("Accessing QueryHistoryRepository Save function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(QueryHistoryData entity)
        {
            Log.LogInformation("Accessing QueryHistoryRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_query_history_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByCode(string entityCode)
        {
            Log.LogInformation("Accessing QueryHistoryRepository DeleteByCode function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@query_history_code", entityCode) };
            pcol.Add(Mapper.GetOutParam());
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_query_history_del_c", pcol);
        }

        public override void DeleteByID(int entityKey)
        {
            Log.LogInformation("Accessing QueryHistoryRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_query_history_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(QueryHistoryData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_query_history_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}
