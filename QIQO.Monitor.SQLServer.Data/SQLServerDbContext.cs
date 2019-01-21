using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;

namespace QIQO.Monitor.SQLServer.Data
{
    public class SqlServerDbContext : DbContextBase, ISqlServerDbContext //, IDisposable
    {
        public SqlServerDbContext(ILogger<DbContextBase> logger, string connectionString) : base(logger, connectionString) { }
    }
}
