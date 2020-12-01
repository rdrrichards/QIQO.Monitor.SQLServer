using QIQO.Monitor.Core;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public class HardwareRepository : ReadRepositoryBase<HardwareData>, IHardwareRepository
    {
        private readonly ISqlServerDbContext entityContext;

        public HardwareRepository(ISqlServerDbContext dbc, IHardwareMap map) : base(map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<HardwareData> Get() =>
            throw new NotAllowedException("Selecting all hardware data with this method is not allowed. Use Get(string queryText) instead.");
        public IEnumerable<HardwareData> Get(string queryText)
        {
            using (entityContext) return MapRows(entityContext.ExecuteSqlStatementAsSqlDataReader(queryText));
        }
    }
}
