using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class WaitStatsLogRepository : RepositoryBase<WaitStatsLogData>, IWaitStatsLogRepository
    {
        private readonly ISqlServerDbContext entityContext;

        public WaitStatsLogRepository(ISqlServerDbContext dbc, IWaitStatsLogMap map, ILogger<WaitStatsLogData> log) : base(log, map)
        {
            entityContext = dbc;
        }
        public IEnumerable<WaitStatsLogData> Get(int serviceKey)
        {
            Log.LogInformation("Accessing WaitStatsLogRepository Get for analysis function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@service_key", serviceKey) };
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_wait_stats_log_get", pcol));
        }
        public IEnumerable<WaitStatsLogData> Get(int serviceKey, int sampleCount)
        {
            Log.LogInformation("Accessing WaitStatsLogRepository Get for analysis function");
            var pcol = new List<SqlParameter>() {
                Mapper.BuildParam("@service_key", serviceKey),
                Mapper.BuildParam("@sample_count", sampleCount)
            };
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_wait_stats_log_get", pcol));
        }
        public IEnumerable<WaitStatsLogData> Get(int serviceKey, DateTime startDate, DateTime endDate)
        {
            Log.LogInformation("Accessing WaitStatsLogRepository Get for analysis function");
            var pcol = new List<SqlParameter>() {
                Mapper.BuildParam("@service_key", serviceKey),
                Mapper.BuildParam("@start_date", startDate),
                Mapper.BuildParam("@end_date", endDate)
            };
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_wait_stats_log_get", pcol));
        }
        public override void Insert(WaitStatsLogData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_wait_stats_log_ins", Mapper.MapParamsForUpsert(entity));
        }
        public override IEnumerable<WaitStatsLogData> GetAll() => throw new NotImplementedException();
        public override void Delete(WaitStatsLogData entity) => throw new NotImplementedException();
        public override void DeleteByID(int entity_key) => throw new NotImplementedException();
        public override WaitStatsLogData GetByID(int entity_key) => throw new NotImplementedException();
        public override void Save(WaitStatsLogData entity) => throw new NotImplementedException();
    }
}
