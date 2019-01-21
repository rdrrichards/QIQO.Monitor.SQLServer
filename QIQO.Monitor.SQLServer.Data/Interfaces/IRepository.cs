using QIQO.Monitor.Core.Contracts;

namespace QIQO.Monitor.SQLServer.Data
{
    public interface IServerRepository : IRepository<ServerData> { }
    public interface IVersionRepository : IRepository<VersionData> {
        VersionData Get();
    }



    public interface IServerMap : IMapper<ServerData> { }
    public interface IVersionMap : IMapper<VersionData> { }
}
