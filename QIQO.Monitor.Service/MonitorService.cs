using Microsoft.Extensions.Logging;
using QIQO.Monitor.Service.Polling;
using QIQO.Monitor.Service.Services;
using QIQO.Monitor.SQLServer.Data;
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
                _logger.LogInformation($"Doing something with {server.ServerName} in the MonitorService");
                // Start basci server level monitors (when we have them built)
                // _pollingServiceFactory.GetPollingService<IBlockingPollingService>().StartPolling(server);

                // Then, pass the server object along to another function that
                // starts monitors for any services on that server
                // _pollingServiceFactory.GetPollingService<IBlockingPollingService>().StartPolling(server.ServiceSource);
                StartSqlMonitors(server.Services);
            });
        }
        private void StartSqlMonitors(List<Service> services)
        {
            services.ForEach(service =>
            {
                _logger.LogInformation($"Doing something with {service.ServiceSource} in the MonitorService");
                // Start basci server level monitors (when we have them built)
                // _pollingServiceFactory.GetPollingService<IBlockingPollingService>().StartPolling(server);

                // Then, pass the server object along to another function that
                // starts monitors for any services on that server
                _pollingServiceFactory.GetPollingService<IBlockingPollingService>().StartPolling(service.ServiceSource);
            });
        }
    }
}
