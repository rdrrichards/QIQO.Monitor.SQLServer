using Microsoft.Extensions.Logging;
using QIQO.Monitor.SQLServer.Data;
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
    public class ServerManager : ManagerBase, IServerManager
    {
        private readonly ICoreCacheService _cacheService;
        private readonly IEnvironmentManager _environmentManager;
        private readonly IServerRepository _serverRepository;
        private readonly IServiceManager _serviceManager;

        public ServerManager(ILogger<ServerManager> logger, ICoreCacheService cacheService,
            IEnvironmentManager environmentManager, IServerRepository serverRepository, IServiceManager serviceManager) : base(logger)
        {
            _cacheService = cacheService;
            _environmentManager = environmentManager;
            _serverRepository = serverRepository;
            _serviceManager = serviceManager;
        }
        public List<Server> GetServers()
        {
            return ExecuteOperation(() =>
            {
                var servers = new List<Server>();
                var serversToMonitor = _cacheService.GetServers().ToList();

                serversToMonitor.ForEach(server =>
                {
                    servers.Add(new Server(server, _serviceManager.GetServices(server.ServerKey),
                        _environmentManager.GetServerEnvironments(server.ServerKey)));
                });

                return servers;
            });
        }
        public Server AddServer(ServerAdd server)
        {
            return ExecuteOperation(() =>
            {
                var endData = new ServerData { ServerName = server.ServerName };
                _serverRepository.Insert(endData);
                _cacheService.RefreshCache();
                return GetServers().FirstOrDefault(e => e.ServerName == server.ServerName);
            });
        }
        public Server UpdateServer(int serverKey, ServerUpdate server)
        {
            return ExecuteOperation(() =>
            {
                var endData = new ServerData { ServerKey = serverKey, ServerName = server.ServerName };
                _serverRepository.Save(endData);
                _cacheService.RefreshCache();
                return GetServers().FirstOrDefault(e => e.ServerKey == serverKey);
            });
        }
        public void DeleteServer(int serverKey)
        {
            ExecuteOperation(() =>
            {
                var endData = new ServerData { ServerKey = serverKey };
                _serverRepository.Delete(endData);
                _cacheService.RefreshCache();
            });
        }
    }
}
