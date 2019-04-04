using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class WaitStatsLogRepository : RepositoryBase<WaitStatsLogData>, IWaitStatsLogRepository
    {
        private readonly IMonitorDbContext entityContext;

        public WaitStatsLogRepository(IMonitorDbContext dbc, IWaitStatsLogMap map, ILogger<WaitStatsLogData> log) : base(log, map)
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
        public override IEnumerable<WaitStatsLogData> GetAll() =>
            throw new NotAllowedException("Selecting all wait stats log data with this method is not allowed. Use Get(string queryText) instead.");
        public override void Delete(WaitStatsLogData entity) =>
            throw new NotAllowedException("Updating wait stats data cannot be deleted");
        public override void DeleteByID(int entity_key) =>
            throw new NotAllowedException("Updating wait stats data cannot be deleted");
        public override WaitStatsLogData GetByID(int entity_key) =>
            throw new NotAllowedException("Selecting wait stats data with this method is not allowed. Use Get(string queryText) instead.");
        public override void Save(WaitStatsLogData entity) =>
            throw new NotAllowedException("Updating wait stats data with is not allowed. Use Insert method instead.");
    }
}
