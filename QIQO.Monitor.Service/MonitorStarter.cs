using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QIQO.Monitor.Service
{
    public class MonitorStarter : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public MonitorStarter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var monitorService =
                    scope.ServiceProvider
                        .GetRequiredService<IMonitorService>();
            }
            return Task.CompletedTask;
        }
    }
}
