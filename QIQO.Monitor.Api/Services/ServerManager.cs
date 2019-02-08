using QIQO.Monitor.SQLServer.Data;
using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Api.Services
{
    public interface IServerManager
    {
        List<Server> GetServers();
    }
    public class ServerManager : IServerManager
    {
        private readonly ICoreCacheService _cacheService;
        private readonly IServerEntityService _serverEntityService;
        private readonly IServiceEntityService _serviceEntityService;
        private readonly IMonitorEntityService _monitorEntityService;
        private readonly IQueryEntityService _queryEntityService;

        public ServerManager(ICoreCacheService cacheService, IServerEntityService serverEntityService, IServiceEntityService serviceEntityService,
            IMonitorEntityService monitorEntityService, IQueryEntityService queryEntityService)
        {
            _cacheService = cacheService;
            _serverEntityService = serverEntityService;
            _serviceEntityService = serviceEntityService;
            _monitorEntityService = monitorEntityService;
            _queryEntityService = queryEntityService;
        }
        public List<Server> GetServers()
        {
            var servers = new List<Server>();
            var serversToMonitor = _cacheService.GetServers().ToList();

            serversToMonitor.ForEach(server =>
            {
                var services = new List<Service>();
                _cacheService.GetServices(server.ServerKey).ToList().ForEach(service =>
                {
                    var monitors = new List<Monitor>();
                    _cacheService.GetMonitors(service.ServiceTypeKey).ToList().ForEach(monitor =>
                    {
                        monitors.Add(new Monitor(monitor, _queryEntityService.Map(_cacheService.GetQueries(monitor.MonitorKey))));
                    });
                    services.Add(new Service(service, monitors));
                });
                servers.Add(new Server(server, services));
            });

            return servers;
        }
    }
}
