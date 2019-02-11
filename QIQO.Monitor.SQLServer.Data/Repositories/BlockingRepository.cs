using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public class BlockingRepository : ReadRepositoryBase<BlockingData>, IBlockingRepository
    {
        private readonly ISqlServerDbContext entityContext;
        // private readonly ICoreCacheService _cacheService;

        public BlockingRepository(ISqlServerDbContext dbc, IBlockingMap map, ILogger<BlockingData> log) : base(log, map)
        {
            entityContext = dbc;
            // _cacheService = cacheService;
        }
        public override IEnumerable<BlockingData> Get() => throw new NotImplementedException();
        public IEnumerable<BlockingData> Get(string queryText)
        {
            // Log.LogInformation("Accessing BlockingRepository Get(queryText) function");
            using (entityContext) return MapRows(entityContext.ExecuteSqlStatementAsSqlDataReader(queryText));
        }

    }
}
