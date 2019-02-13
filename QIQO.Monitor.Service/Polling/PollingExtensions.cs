using Microsoft.Extensions.DependencyInjection;
using QIQO.Monitor.Service.Services;

namespace QIQO.Monitor.Service.Polling
{
    public static class PollingExtensions
    {
        public static IServiceCollection AddPollers(this IServiceCollection services)
        {
            services.AddSingleton<IPollingServiceFactory>(new PollingServiceFactory(services));
            services.AddScoped<IBlockingPollingService, BlockingPollingService>();
            services.AddScoped<IOpenTransactionPollingService, OpenTransactionPollingService>();
            return services;
        }
    }
}
