using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public class OpenTranactionRepository : ReadRepositoryBase<OpenTranactionData>, IOpenTranactionRepository
    {
        private readonly ISqlServerDbContext entityContext;
        private readonly ICoreCacheService _cacheService;

        public OpenTranactionRepository(ISqlServerDbContext dbc, IOpenTranactionMap map,
            ILogger<OpenTranactionData> log, ICoreCacheService cacheService) : base(log, map)
        {
            entityContext = dbc;
            _cacheService = cacheService;
        }

        public override IEnumerable<OpenTranactionData> Get()
        {
            Log.LogInformation("Accessing OpenTranactionRepository Get function");
            var sql = _cacheService.GetQuery("Open Tranactions", 1);
            using (entityContext) return MapRows(entityContext.ExecuteSqlStatementAsSqlDataReader(sql.QueryText));
        }
    }
}
