using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.SQLServer.Data;
using System;

namespace QIQO.Monitor.Api
{
    public interface IServerEntityService : IEntityService<Server, ServerData> { }
    public interface IServiceEntityService : IEntityService<Service, ServiceData> { }
    public interface IMonitorEntityService : IEntityService<Monitor, MonitorData> { }
    public interface IQueryEntityService : IEntityService<Query, QueryData> { }
    public interface IEnvironmentEntityService : IEntityService<Environment, EnvironmentData> { }
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
    public class EnvironmentEntityService : IEnvironmentEntityService
    {
        public Environment Map(EnvironmentData ent) => new Environment(ent);

        public EnvironmentData Map(Environment ent) => throw new NotImplementedException();
    }
}
