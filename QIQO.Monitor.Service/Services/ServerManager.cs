using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.SQLServer.Data;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Service
{
    public interface IServerManager
    {
        List<Server> GetServers();
    }
    public class ServerManager : IServerManager
    {
        private readonly ICoreCacheService _cacheService;
        private readonly IQueryEntityService _queryEntityService;
        private readonly IEnvironmentEntityService _environmentEntityService;

        public ServerManager(ICoreCacheService cacheService, IQueryEntityService queryEntityService,
            IEnvironmentEntityService environmentEntityService)
        {
            _cacheService = cacheService;
            _queryEntityService = queryEntityService;
            _environmentEntityService = environmentEntityService;
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
                    services.Add(new Service(service, monitors, _environmentEntityService.Map(_cacheService.GetServiceEnvironments(service.ServiceKey))));
                });
                servers.Add(new Server(server, services, _environmentEntityService.Map(_cacheService.GetServerEnvironments(server.ServerKey))));
            });

            return servers;
        }
    }
}
