using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace QIQO.Monitor.SQLServer.Data
{
    public static class DataExtensions
    {
        public static IServiceCollection AddSqlServerDataAccessServices(this IServiceCollection services, 
            Action<DataAccessOptions> configuration = null)
        {
            services.AddSingleton<IDbContextFactory, DbContextFactory>();
            services.AddSingleton<IDataRepositoryFactory, DataRepositoryFactory>();

            services.AddTransient<IVersionMap, VersionMap>();
            services.AddTransient<IVersionRepository, VersionRepository>();
            services.AddTransient<IHardwareMap, HardwareMap>();
            services.AddTransient<IHardwareRepository, HardwareRepository>();
            services.AddTransient<IBlockingMap, BlockingMap>();
            services.AddTransient<IBlockingRepository, BlockingRepository>();
            services.AddTransient<IOpenTranactionMap, OpenTranactionMap>();
            services.AddTransient<IOpenTransactionRepository, OpenTranactionRepository>();

            services.AddTransient<IWaitStatsMap, WaitStatsMap>();
            services.AddTransient<IWaitStatsRepository, WaitStatsRepository>();
            services.AddTransient<IWaitStatsLogMap, WaitStatsLogMap>();
            services.AddTransient<IWaitStatsLogRepository, WaitStatsLogRepository>();

            return services;
        }
    }
    public class DataAccessOptions
    {
        public string ConnectionString { get; set; }
    }
}
