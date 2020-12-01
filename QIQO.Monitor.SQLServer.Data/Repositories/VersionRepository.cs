using QIQO.Monitor.Core;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public class VersionRepository : ReadRepositoryBase<VersionData>,
                                     IVersionRepository
    {
        private readonly ISqlServerDbContext entityContext;
        public VersionRepository(ISqlServerDbContext dbc, IVersionMap map) : base(map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<VersionData> Get()
        {
            using (entityContext) return MapRows(entityContext.ExecuteSqlStatementAsSqlDataReader("SELECT @@VERSION AS version_text"));
        }
    }
}
