using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QIQO.Monitor.Service
{
    public class MonitorDataProcessorStarter : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public MonitorDataProcessorStarter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var monitorService =
                    scope.ServiceProvider
                        .GetRequiredService<IMonitorDataProcessorService>();
            }
            return Task.CompletedTask;
        }
    }
}
