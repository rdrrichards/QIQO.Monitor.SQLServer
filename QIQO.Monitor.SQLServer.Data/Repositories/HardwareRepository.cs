using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public class HardwareRepository : ReadRepositoryBase<HardwareData>,
                                     IHardwareRepository
    {
        private readonly ISqlServerDbContext entityContext;
        public HardwareRepository(ISqlServerDbContext dbc, IHardwareMap map,
            ILogger<HardwareData> log, ICacheService cacheService) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<HardwareData> Get() => throw new NotImplementedException();
    }
}
