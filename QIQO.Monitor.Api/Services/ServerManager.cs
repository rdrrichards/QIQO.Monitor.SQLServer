using QIQO.Monitor.SQLServer.Data;
using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

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
        private readonly IQueryEntityService _queryEntityService;
        private readonly IEnvironmentEntityService _environmentEntityService;
        private readonly IServerRepository _serverRepository;

        public ServerManager(ILogger<ServerManager> logger, ICoreCacheService cacheService, IQueryEntityService queryEntityService,
            IEnvironmentEntityService environmentEntityService, IServerRepository serverRepository) : base(logger)
        {
            _cacheService = cacheService;
            _queryEntityService = queryEntityService;
            _environmentEntityService = environmentEntityService;
            _serverRepository = serverRepository;
        }
        public List<Server> GetServers()
        {
            return ExecuteOperation(() =>
            {
                var servers = new List<Server>();
                var serversToMonitor = _cacheService.GetServers().ToList();

                serversToMonitor.ForEach(server =>
                {
                    var services = new List<Service>();
                    _cacheService.GetServices(server.ServerKey).ToList().ForEach(service =>
                    {
                        var monitors = new List<Monitor>();
                        _cacheService.GetServiceMonitors(service.ServiceKey).ToList().ForEach(monitor =>
                        {
                            monitors.Add(new Monitor(monitor, _queryEntityService.Map(_cacheService.GetQueries(monitor.MonitorKey)),
                                GetMonitorProperties(service.ServiceKey, monitor.MonitorKey)));
                        });
                        services.Add(new Service(service, monitors, _environmentEntityService.Map(_cacheService.GetServiceEnvironments(service.ServiceKey))));
                    });
                    servers.Add(new Server(server, services, _environmentEntityService.Map(_cacheService.GetServerEnvironments(server.ServerKey))));
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
        private List<MonitorProperty> GetMonitorProperties(int serviceKey, int monitorKey)
        {
            return _cacheService.GetServiceMonitorAttributes(serviceKey, monitorKey).ToList()
                .Join(_cacheService.GetAttributeTypes(), a => a.AttributeTypeKey, t => t.AttributeTypeKey, (a, t)
                    => new { PropertyType = t.AttributeTypeName, PropertyValue = a.AttributeValue, t.AttributeDataTypeKey })
                    .Join(_cacheService.GetAttributeDataTypes(), n => n.AttributeDataTypeKey, d => d.AttributeDataTypeKey, (n, d)
                    => new MonitorProperty(n.PropertyType, d.AttributeDataTypeName, n.PropertyValue)).ToList();
        }
    }
}
