using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public class OpenTranactionRepository : ReadRepositoryBase<OpenTranactionData>, IOpenTranactionRepository
    {
        private readonly ISqlServerDbContext entityContext;

        public OpenTranactionRepository(ISqlServerDbContext dbc, IOpenTranactionMap map,
            ILogger<OpenTranactionData> log) : base(log, map)
        {
            entityContext = dbc;
        }
        public override IEnumerable<OpenTranactionData> Get() => throw new NotImplementedException();
        public IEnumerable<OpenTranactionData> Get(string queryText)
        {
            Log.LogInformation("Accessing OpenTranactionRepository Get function");
            using (entityContext) return MapRows(entityContext.ExecuteSqlStatementAsSqlDataReader(queryText));
        }
    }
}
