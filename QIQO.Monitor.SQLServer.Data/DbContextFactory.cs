using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
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
        private readonly ILogger<DbContextBase> _logger;
        private readonly IServiceCollection _services;

        public DbContextFactory(IServiceCollection services)
        {
            _services = services;
            var serviceCol = _services.BuildServiceProvider();
            _logger = serviceCol.GetService<ILogger<DbContextBase>>();
        }
        public void Create(string connectionString) => _services.AddTransient<ISqlServerDbContext>(_ => new SqlServerDbContext(_logger, connectionString));
    }
}
