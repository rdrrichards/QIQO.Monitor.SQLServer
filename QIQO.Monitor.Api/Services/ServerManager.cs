using QIQO.Monitor.SQLServer.Data;
using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Api.Services
{
    public interface IServerManager
    {
        List<Server> GetServers();
        Server AddServer(ServerAdd environment);
        Server UpdateServer(int environmentKey, ServerUpdate environment);
        void DeleteServer(int environmentKey);
    }
    public class ServerManager : IServerManager
    {
        private readonly ICoreCacheService _cacheService;
        private readonly IQueryEntityService _queryEntityService;
        private readonly IEnvironmentEntityService _environmentEntityService;
        private readonly IServerRepository _serverRepository;

        public ServerManager(ICoreCacheService cacheService, IQueryEntityService queryEntityService,
            IEnvironmentEntityService environmentEntityService, IServerRepository serverRepository)
        {
            _cacheService = cacheService;
            _queryEntityService = queryEntityService;
            _environmentEntityService = environmentEntityService;
            _serverRepository = serverRepository;
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
        public Server AddServer(ServerAdd environment)
        {
            var endData = new ServerData { ServerName = environment.ServerName };
            _serverRepository.Insert(endData);
            _cacheService.RefreshCache();
            return GetServers().FirstOrDefault(e => e.ServerName == environment.ServerName);
        }
        public Server UpdateServer(int environmentKey, ServerUpdate environment)
        {
            var endData = new ServerData { ServerKey = environmentKey, ServerName = environment.ServerName };
            _serverRepository.Save(endData);
            _cacheService.RefreshCache();
            return GetServers().FirstOrDefault(e => e.ServerKey == environmentKey);
        }
        public void DeleteServer(int environmentKey)
        {
            var endData = new ServerData { ServerKey = environmentKey };
            _serverRepository.Delete(endData);
            _cacheService.RefreshCache();
        }
    }
}
