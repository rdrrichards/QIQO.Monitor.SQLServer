using Microsoft.Extensions.DependencyInjection;
using QIQO.Monitor.Core.Contracts;

namespace QIQO.Monitor.SQLServer.Data
{

    public interface ISqlServerDbContext : IDbContext { }
    public interface IDbContextFactory
    {
        void Create(string connectionString);
    }
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IServiceCollection _services;

        public DbContextFactory(IServiceCollection services)
        {
            _services = services;
        }
        public void Create(string connectionString) => _services.AddTransient<ISqlServerDbContext>(_ => new SqlServerDbContext(connectionString));
    }
}
