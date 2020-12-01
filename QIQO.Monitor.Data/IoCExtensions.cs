using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace QIQO.Monitor.Data
{
    public static class DataExtensions
    {
        public static IServiceCollection AddApplicationDataAccessServices(this IServiceCollection services,
            Action<DataAccessOptions> configuration = null)
        {
            services.AddTransient<IMonitorDbContext>(serviceProvider => {
                var optionsProvider = serviceProvider.GetService<IOptions<DataAccessOptions>>();
                var options = optionsProvider.Value;

                // Allow the developer to perform further configuration
                configuration?.Invoke(options);

                if (string.IsNullOrEmpty(options.ConnectionString))
                {
                    throw new InvalidOperationException($"No {nameof(DataAccessOptions.ConnectionString)} " +
                        $"was set on the {nameof(DataAccessOptions)}.");
                }
                return new MonitorDbContext(options.ConnectionString);
            });

            services.AddSingleton<ICoreCacheService, CoreCacheService>();

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
            services.AddTransient<IServiceMonitorMap, ServiceMonitorMap>();
            services.AddTransient<IServiceMonitorRepository, ServiceMonitorRepository>();


            services.AddTransient<IServiceMonitorAttributeMap, ServiceMonitorAttributeMap>();
            services.AddTransient<IServiceMonitorAttributeRepository, ServiceMonitorAttributeRepository>();
            services.AddTransient<IAttributeTypeMap, AttributeTypeMap>();
            services.AddTransient<IAttributeTypeRepository, AttributeTypeRepository>();
            services.AddTransient<IAttributeDataTypeMap, AttributeDataTypeMap>();
            services.AddTransient<IAttributeDataTypeRepository, AttributeDataTypeRepository>();

            services.AddTransient<IEnvironmentMap, EnvironmentMap>();
            services.AddTransient<IEnvironmentRepository, EnvironmentRepository>();
            services.AddTransient<IEnvironmentServerMap, EnvironmentServerMap>();
            services.AddTransient<IEnvironmentServerRepository, EnvironmentServerRepository>();
            services.AddTransient<IEnvironmentServiceMap, EnvironmentServiceMap>();
            services.AddTransient<IEnvironmentServiceRepository, EnvironmentServiceRepository>();
            return services;
        }
        public static void AddDataServices(this IServiceCollection services, Action<DataAccessOptions> configuration = null)
        {
            AddApplicationDataAccessServices(services, configuration);
        }
    }
    public class DataAccessOptions
    {
        public string ConnectionString { get; set; }
    }
}
