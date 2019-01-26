using Microsoft.Extensions.DependencyInjection;

namespace QIQO.Monitor.SQLServer.Data
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddSingleton<ICoreCacheService, CoreCacheService>();
            services.AddSingleton<IDbContextFactory>(new DbContextFactory(services));
            services.AddSingleton<IDataRepositoryFactory>(new DataRepositoryFactory(services));
            services.AddTransient<IMonitorDbContext, MonitorDbContext>();

            services.AddTransient<IServerMap, ServerMap>();
            services.AddTransient<IServerRepository, ServerRepository>();
            services.AddTransient<IQueryMap, QueryMap>();
            services.AddTransient<IQueryRepository, QueryRepository>();
            services.AddTransient<ILevelMap, LevelMap>();
            services.AddTransient<ILevelRepository, LevelRepository>();
            services.AddTransient<ICategoryMap, CategoryMap>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<IVersionMap, VersionMap>();
            services.AddTransient<IVersionRepository, VersionRepository>();
            services.AddTransient<IHardwareMap, HardwareMap>();
            services.AddTransient<IHardwareRepository, HardwareRepository>();
            return services;
        }
    }
}
