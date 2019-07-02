using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public class OpenTranactionRepository : ReadRepositoryBase<OpenTransactionData>, IOpenTransactionRepository
    {
        private readonly ISqlServerDbContext entityContext;

        public OpenTranactionRepository(ISqlServerDbContext dbc, IOpenTranactionMap map,
            ILogger<OpenTransactionData> log) : base(log, map)
        {
            entityContext = dbc;
        }
        public override IEnumerable<OpenTransactionData> Get() =>
            throw new NotAllowedException("Selecting all open transaction data with this method is not allowed. Use Get(string queryText) instead.");
        public IEnumerable<OpenTransactionData> Get(string queryText)
        {
            // Log.LogInformation("Accessing OpenTranactionRepository Get function");
            using (entityContext) return MapRows(entityContext.ExecuteSqlStatementAsSqlDataReader(queryText));
        }
    }
}
