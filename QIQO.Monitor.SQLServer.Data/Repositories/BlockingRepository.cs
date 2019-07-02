using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public class BlockingRepository : ReadRepositoryBase<BlockingData>, IBlockingRepository
    {
        private readonly ISqlServerDbContext entityContext;

        public BlockingRepository(ISqlServerDbContext dbc, IBlockingMap map, ILogger<BlockingData> log) : base(log, map)
        {
            entityContext = dbc;
        }
        public override IEnumerable<BlockingData> Get() =>
            throw new NotAllowedException("Selecting all blocking data with this method is not allowed. Use Get(string queryText) instead.");
        public IEnumerable<BlockingData> Get(string queryText)
        {
            using (entityContext) return MapRows(entityContext.ExecuteSqlStatementAsSqlDataReader(queryText));
        }

    }
}
