using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.SQLServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Service
{
    public interface IServerEntityService : IEntityService<Server, ServerData> { }
    public interface IServiceEntityService : IEntityService<Service, ServiceData> { }
    public interface IMonitorEntityService : IEntityService<Monitor, MonitorData> { }
    public interface IQueryEntityService : IEntityService<Query, QueryData> { }
    public class ServerEntityService : IServerEntityService
    {
        public Server Map(ServerData ent) => new Server(ent);

        public ServerData Map(Server ent) => throw new NotImplementedException();
    }
    public class ServiceEntityService : IServiceEntityService
    {
        public Service Map(ServiceData ent) => new Service(ent);

        public ServiceData Map(Service ent) => throw new NotImplementedException();
    }
    public class MonitorEntityService : IMonitorEntityService
    {
        public Monitor Map(MonitorData ent) => new Monitor(ent);

        public MonitorData Map(Monitor ent) => throw new NotImplementedException();
    }
    public class QueryEntityService : IQueryEntityService
    {
        public Query Map(QueryData ent) => new Query(ent);

        public QueryData Map(Query ent) => throw new NotImplementedException();
    }
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
