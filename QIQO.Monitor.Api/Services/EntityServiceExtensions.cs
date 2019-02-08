using Microsoft.Extensions.DependencyInjection;

namespace QIQO.Monitor.Api.Services
{
    public static class EntityServiceExtensions
    {
        public static IServiceCollection AddEntityServices(this IServiceCollection services)
        {
            services.AddTransient<IServerManager, ServerManager>();
            services.AddTransient<IServerEntityService, ServerEntityService>();
            services.AddTransient<IServiceEntityService, ServiceEntityService>();
            services.AddTransient<IMonitorEntityService, MonitorEntityService>();
            services.AddTransient<IQueryEntityService, QueryEntityService>();

            return services.AddTransient<IBlockingEntityService, BlockingEntityService>()
                .AddTransient<IOpenTranactionEntityService, OpenTranactionEntityService>();
        }
    }
}
