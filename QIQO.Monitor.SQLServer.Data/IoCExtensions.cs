using Microsoft.Extensions.DependencyInjection;

namespace QIQO.Monitor.SQLServer.Data
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddSingleton<IDbContextFactory>(new DbContextFactory(services));
            services.AddSingleton<IDataRepositoryFactory>(new DataRepositoryFactory(services));
            services.AddTransient<IMonitorDbContext, MonitorDbContext>();

            services.AddTransient<IServerMap, ServerMap>();
            services.AddTransient<IServerRepository, ServerRepository>();
            services.AddTransient<IVersionMap, VersionMap>();
            services.AddTransient<IVersionRepository, VersionRepository>();
            return services;
        }
    }
}
