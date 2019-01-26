using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public class HardwareRepository : ReadRepositoryBase<HardwareData>, IHardwareRepository
    {
        private readonly ISqlServerDbContext entityContext;
        private readonly ICoreCacheService _cacheService;

        public HardwareRepository(ISqlServerDbContext dbc, IHardwareMap map,
            ILogger<HardwareData> log, ICoreCacheService cacheService) : base(log, map)
        {
            entityContext = dbc;
            _cacheService = cacheService;
        }

        public override IEnumerable<HardwareData> Get()
        {
            Log.LogInformation("Accessing HardwareRepository Get function");
            var sql = _cacheService.GetQuery("SQL Server Hardware", 1);
            using (entityContext) return MapRows(entityContext.ExecuteSqlStatementAsSqlDataReader(sql.QueryText));
        }
    }
}
