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
            services.AddSingleton<IMonitorDbContext, MonitorDbContext>();

            services.AddTransient<IServerMap, ServerMap>();
            services.AddTransient<IServerRepository, ServerRepository>();
            services.AddTransient<IQueryMap, QueryMap>();
            services.AddTransient<IQueryRepository, QueryRepository>();
            services.AddTransient<ILevelMap, LevelMap>();
            services.AddTransient<ILevelRepository, LevelRepository>();
            services.AddTransient<ICategoryMap, CategoryMap>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();


            services.AddTransient<IMonitorMap, MonitorMap>();
            services.AddTransient<IMonitorRepository, MonitorRepository>();
            services.AddTransient<IMonitorTypeMap, MonitorTypeMap>();
            services.AddTransient<IMonitorTypeRepository, MonitorTypeRepository>();
            services.AddTransient<IMonitorQueryMap, MonitorQueryMap>();
            services.AddTransient<IMonitorQueryRepository, MonitorQueryRepository>();
            services.AddTransient<IQueryHistoryMap, QueryHistoryMap>();
            services.AddTransient<IQueryHistoryRepository, QueryHistoryRepository>();
            services.AddTransient<IServiceMap, ServiceMap>();
            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<IServiceTypeMap, ServiceTypeMap>();
            services.AddTransient<IServiceTypeRepository, ServiceTypeRepository>();

            services.AddTransient<IVersionMap, VersionMap>();
            services.AddTransient<IVersionRepository, VersionRepository>();
            services.AddTransient<IHardwareMap, HardwareMap>();
            services.AddTransient<IHardwareRepository, HardwareRepository>();
            services.AddTransient<IBlockingMap, BlockingMap>();
            services.AddTransient<IBlockingRepository, BlockingRepository>();
            services.AddTransient<IOpenTranactionMap, OpenTranactionMap>();
            services.AddTransient<IOpenTransactionRepository, OpenTranactionRepository>();
            return services;
        }
    }
}
