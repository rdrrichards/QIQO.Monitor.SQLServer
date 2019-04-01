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

            services.AddTransient<IServerEntityService, ServerEntityService>();
            services.AddTransient<IServiceEntityService, ServiceEntityService>();
            services.AddTransient<IMonitorEntityService, MonitorEntityService>();
            services.AddTransient<IQueryEntityService, QueryEntityService>();
            services.AddTransient<IEnvironmentEntityService, EnvironmentEntityService>();

            return services.AddTransient<IBlockingEntityService, BlockingEntityService>()
                .AddTransient<IOpenTransactionEntityService, OpenTransactionEntityService>()
                .AddTransient<IWaitStatsEntityService, WaitStatsEntityService>();
        }
    }
}
