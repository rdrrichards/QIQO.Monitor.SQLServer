using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public class WaitStatsRepository : ReadRepositoryBase<WaitStatsData>, IWaitStatsRepository
    {
        private readonly ISqlServerDbContext entityContext;
        // private readonly ICoreCacheService _cacheService;

        public WaitStatsRepository(ISqlServerDbContext dbc, IWaitStatsMap map, ILogger<WaitStatsData> log) : base(log, map)
        {
            entityContext = dbc;
            // _cacheService = cacheService;
        }
        public override IEnumerable<WaitStatsData> Get() =>
            throw new NotAllowedException("Selecting all wait stats data with this method is not allowed. Use Get(string queryText) instead.");
        public IEnumerable<WaitStatsData> Get(string queryText)
        {
            // Log.LogInformation("Accessing BlockingRepository Get(queryText) function");
            using (entityContext) return MapRows(entityContext.ExecuteSqlStatementAsSqlDataReader(queryText));
        }

    }
}
