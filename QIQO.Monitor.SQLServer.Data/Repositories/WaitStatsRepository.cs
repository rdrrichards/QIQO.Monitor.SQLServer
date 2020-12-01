using QIQO.Monitor.Core;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public class WaitStatsRepository : ReadRepositoryBase<WaitStatsData>, IWaitStatsRepository
    {
        private readonly ISqlServerDbContext entityContext;

        public WaitStatsRepository(ISqlServerDbContext dbc, IWaitStatsMap map) : base(map)
        {
            entityContext = dbc;
        }
        public override IEnumerable<WaitStatsData> Get() =>
            throw new NotAllowedException("Selecting all wait stats data with this method is not allowed. Use Get(string queryText) instead.");
        public IEnumerable<WaitStatsData> Get(string queryText)
        {
            using (entityContext) return MapRows(entityContext.ExecuteSqlStatementAsSqlDataReader(queryText));
        }

    }
}
