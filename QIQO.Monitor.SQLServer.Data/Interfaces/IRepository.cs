using QIQO.Monitor.Core.Contracts;

namespace QIQO.Monitor.SQLServer.Data
{
    public interface IServerRepository : IRepository<ServerData> { }
    public interface IQueryRepository : IRepository<QueryData> { }
    public interface ILevelRepository : IRepository<LevelData> { }
    public interface ICategoryRepository : IRepository<CategoryData> { }
    public interface IVersionRepository : IReadRepository<VersionData> { }
    public interface IHardwareRepository : IReadRepository<HardwareData> { }
    public interface IBlockingRepository : IReadRepository<BlockingData> { }
    public interface IOpenTranactionRepository : IReadRepository<OpenTranactionData> { }

    public interface IServerMap : IMapper<ServerData> { }
    public interface IQueryMap : IMapper<QueryData> { }
    public interface ILevelMap : IMapper<LevelData> { }
    public interface ICategoryMap : IMapper<CategoryData> { }
    public interface IVersionMap : IReadMapper<VersionData> { }
    public interface IHardwareMap : IReadMapper<HardwareData> { }
    public interface IBlockingMap : IReadMapper<BlockingData> { }
    public interface IOpenTranactionMap : IReadMapper<OpenTranactionData> { }
}
