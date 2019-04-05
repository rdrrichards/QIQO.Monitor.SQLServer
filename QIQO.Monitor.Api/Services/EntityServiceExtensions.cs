using Microsoft.Extensions.DependencyInjection;

namespace QIQO.Monitor.Api.Services
{
    public static class EntityServiceExtensions
    {
        public static IServiceCollection AddEntityServices(this IServiceCollection services)
        {
            services.AddTransient<IServerManager, ServerManager>();
            services.AddTransient<IServiceManager, ServiceManager>();
            services.AddTransient<IEnvironmentManager, EnvironmentManager>();
            services.AddTransient<IMonitorManager, MonitorManager>();
            services.AddTransient<IQueryManager, QueryManager>();

            services.AddTransient<IServerEntityService, ServerEntityService>();
            services.AddTransient<IServiceEntityService, ServiceEntityService>();
            services.AddTransient<IEnvironmentEntityService, EnvironmentEntityService>();
            services.AddTransient<IMonitorEntityService, MonitorEntityService>();
            services.AddTransient<IQueryEntityService, QueryEntityService>();

            return services.AddTransient<IBlockingEntityService, BlockingEntityService>()
                .AddTransient<IOpenTransactionEntityService, OpenTransactionEntityService>()
                .AddTransient<IWaitStatsEntityService, WaitStatsEntityService>();
        }
    }
}
