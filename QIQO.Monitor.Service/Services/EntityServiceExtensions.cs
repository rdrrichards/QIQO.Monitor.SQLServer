using Microsoft.Extensions.DependencyInjection;

namespace QIQO.Monitor.Service
{
    public static class EntityServiceExtensions
    {
        public static IServiceCollection AddEntityServices(this IServiceCollection services)
        {
            services.AddTransient<IServerEntityService, ServerEntityService>();
            services.AddTransient<IServiceEntityService, ServiceEntityService>();
            services.AddTransient<IMonitorEntityService, MonitorEntityService>();
            services.AddTransient<IQueryEntityService, QueryEntityService>();
            services.AddTransient<IEnvironmentEntityService, EnvironmentEntityService>();
            return services;
        }
    }
}
