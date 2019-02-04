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
                servers.Add(new Server(server, _serviceEntityService.Map(_cacheService.GetServices(server.ServerKey))));
            });

            return servers;
        }
    }
}
