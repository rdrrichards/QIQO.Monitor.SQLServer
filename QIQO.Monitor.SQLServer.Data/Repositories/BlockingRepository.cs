using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public class BlockingRepository : ReadRepositoryBase<BlockingData>, IBlockingRepository
    {
        private readonly ISqlServerDbContext entityContext;
        private readonly ICoreCacheService _cacheService;

        public BlockingRepository(ISqlServerDbContext dbc, IBlockingMap map,
            ILogger<BlockingData> log, ICoreCacheService cacheService) : base(log, map)
        {
            entityContext = dbc;
            _cacheService = cacheService;
        }

        public override IEnumerable<BlockingData> Get()
        {
            Log.LogInformation("Accessing BlockingRepository Get function");
            var sql = _cacheService.GetQuery("Detect Blocking", 1);
            using (entityContext) return MapRows(entityContext.ExecuteSqlStatementAsSqlDataReader(sql.QueryText));
        }
    }
}
