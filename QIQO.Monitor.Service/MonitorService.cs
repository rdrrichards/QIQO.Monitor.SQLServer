using Microsoft.Extensions.Logging;
using QIQO.Monitor.Service.Polling;
using QIQO.Monitor.Service.Services;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Service
{
    public interface IMonitorService { }
    public class MonitorService : IMonitorService
    {
        private readonly IServerManager _serverManager;
        private readonly ILogger<MonitorService> _logger;
        private readonly IPollingServiceFactory _pollingServiceFactory;

        public MonitorService(IServerManager serverManager, ILogger<MonitorService> logger, IPollingServiceFactory pollingServiceFactory)
        {
            _serverManager = serverManager;
            _logger = logger;
            _pollingServiceFactory = pollingServiceFactory;
            StartMonitors();
        }
        private void StartMonitors()
        {
            var serversToMonitor = _serverManager.GetServers();
            // var queries = _cacheService.GetQueries().ToList();
            serversToMonitor.ForEach(server =>
            {
                _logger.LogInformation($"Beginning to monitor {server.ServerName}");
                // Start basic server level monitors (when we have them built)
                // _pollingServiceFactory.GetPollingService<IBlockingPollingService>().StartPolling(server);

                // Then, pass the server object along to another function that
                // starts monitors for any services on that server
                StartSqlMonitors(server);
            });
        }
        private void StartSqlMonitors(Server server)
        {
            server.Services.Where(s => s.ServiceType == ServiceType.SqlServer).ToList().ForEach(service =>
            {
                _logger.LogInformation($"Beginning to monitor Sql Server {service.ServiceSource}");
                service.Monitors.ForEach(monitor => 
                {
                    switch (monitor.MonitorCategory)
                    {
                        case MonitorCategory.Version:
                            break;
                        case MonitorCategory.SQLServerHardware:
                            break;
                        case MonitorCategory.DetectBlocking:
                            _pollingServiceFactory.GetPollingService<IBlockingPollingService>().StartPolling(server, service);
                            break;
                        case MonitorCategory.OpenTranactions:
                            _pollingServiceFactory.GetPollingService<IOpenTransactionPollingService>().StartPolling(server, service);
                            break;
                        case MonitorCategory.WaitStats:
                            _pollingServiceFactory.GetPollingService<IWaitStatsPollingService>().StartPolling(server, service);
                            break;
                        default:
                            break;
                    }
                });                
            });
        }
    }
}
