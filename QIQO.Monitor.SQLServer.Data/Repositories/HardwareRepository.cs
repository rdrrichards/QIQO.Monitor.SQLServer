using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
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

        public override IEnumerable<HardwareData> Get() =>
            throw new NotAllowedException("Selecting all hardware data with this method is not allowed. Use Get(string queryText) instead.");
        public IEnumerable<HardwareData> Get(string queryText)
        {
            Log.LogInformation("Accessing OpenTranactionRepository Get function");
            using (entityContext) return MapRows(entityContext.ExecuteSqlStatementAsSqlDataReader(queryText));
        }
    }
}
