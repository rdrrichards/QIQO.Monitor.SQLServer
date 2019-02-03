using Microsoft.Extensions.Logging;
using QIQO.Monitor.Service.Polling;
using QIQO.Monitor.Service.Services;
using QIQO.Monitor.SQLServer.Data;
using System.Linq;

namespace QIQO.Monitor.Service
{
    public interface IMonitorService { }
    public class MonitorService : IMonitorService
    {
        private readonly ICoreCacheService _cacheService;
        private readonly ILogger<MonitorService> _logger;
        private readonly IPollingServiceFactory _pollingServiceFactory;

        public MonitorService(ICoreCacheService cacheService, ILogger<MonitorService> logger, IPollingServiceFactory pollingServiceFactory)
        {
            _cacheService = cacheService;
            _logger = logger;
            _pollingServiceFactory = pollingServiceFactory;
            StartMonitors();
        }
        private void StartMonitors()
        {
            var serversToMonitor =_cacheService.GetServers().ToList();
            var queries = _cacheService.GetQueries().ToList();
            serversToMonitor.ForEach(server =>
            {
                _logger.LogInformation($"Doing something with {server.ServerName} in the MonitorService");
                // Start basci server level monitors (when we have them built)
                // _pollingServiceFactory.GetPollingService<IBlockingPollingService>().StartPolling(server);

                // Then, pass the server object along to another function that
                // starts monitors for any services on that server
                _pollingServiceFactory.GetPollingService<IBlockingPollingService>().StartPolling(server.ServerSource);
            });
        }
    }
}
