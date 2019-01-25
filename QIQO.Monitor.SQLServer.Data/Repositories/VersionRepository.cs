using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public class VersionRepository : ReadRepositoryBase<VersionData>,
                                     IVersionRepository
    {
        private readonly ISqlServerDbContext entityContext;
        public VersionRepository(ISqlServerDbContext dbc, IVersionMap map, ILogger<VersionData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<VersionData> Get()
        {
            Log.LogInformation("Accessing VersionRepository Get function");
            using (entityContext) return MapRows(entityContext.ExecuteSqlStatementAsSqlDataReader("SELECT @@VERSION AS version_text"));
        }
    }
}
