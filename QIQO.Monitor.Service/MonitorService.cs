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
            var servicesToMonitor =_cacheService.GetServers().ToList();
            var queries = _cacheService.GetQueries().ToList();
            servicesToMonitor.ForEach(s =>
            {
                _logger.LogInformation($"Doing something with {s.ServerName} in the MonitorService");
                _pollingServiceFactory.GetPollingService<IBlockingPollingService>().StartPolling(s.ServerSource);
            });
        }
    }
}
