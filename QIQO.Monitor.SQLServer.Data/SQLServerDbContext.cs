using QIQO.Monitor.Core;

namespace QIQO.Monitor.SQLServer.Data
{
    public class SqlServerDbContext : DbContextBase, ISqlServerDbContext //, IDisposable
    {
        public SqlServerDbContext(string connectionString) : base(connectionString) { }
    }
}
