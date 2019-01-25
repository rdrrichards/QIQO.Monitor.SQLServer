using QIQO.Monitor.Core.Contracts;

namespace QIQO.Monitor.SQLServer.Data
{
    public interface IServerRepository : IRepository<ServerData> { }
    public interface IVersionRepository : IReadRepository<VersionData> { }

    public interface IHardwareRepository : IReadRepository<HardwareData> { }

    public interface IServerMap : IMapper<ServerData> { }
    public interface IVersionMap : IMapper<VersionData> { }
    public interface IHardwareMap : IMapper<HardwareData> { }
}
