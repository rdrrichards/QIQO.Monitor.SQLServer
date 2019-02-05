﻿using QIQO.Monitor.Core.Contracts;

namespace QIQO.Monitor.SQLServer.Data
{
    public interface IServerRepository : IRepository<ServerData> { }
    public interface IQueryRepository : IRepository<QueryData> { }
    public interface ILevelRepository : IRepository<LevelData> { }
    public interface ICategoryRepository : IRepository<CategoryData> { }
    public interface IMonitorRepository : IRepository<MonitorData> { }
    public interface IMonitorTypeRepository : IRepository<MonitorTypeData> { }
    public interface IMonitorQueryRepository : IRepository<MonitorQueryData> { }
    public interface IQueryHistoryRepository : IRepository<QueryHistoryData> { }
    public interface IServiceRepository : IRepository<ServiceData> { }
    public interface IServiceTypeRepository : IRepository<ServiceTypeData> { }


    public interface IServerMap : IMapper<ServerData> { }
    public interface IQueryMap : IMapper<QueryData> { }
    public interface ILevelMap : IMapper<LevelData> { }
    public interface ICategoryMap : IMapper<CategoryData> { }
    public interface IMonitorMap : IMapper<MonitorData> { }
    public interface IMonitorQueryMap : IMapper<MonitorQueryData> { }
    public interface IMonitorTypeMap : IMapper<MonitorTypeData> { }
    public interface IQueryHistoryMap : IMapper<QueryHistoryData> { }
    public interface IServiceMap : IMapper<ServiceData> { }
    public interface IServiceTypeMap : IMapper<ServiceTypeData> { }

    // Monitor results related interfaces
    public interface IVersionRepository : IReadRepository<VersionData> { }
    public interface IHardwareRepository : IReadRepository<HardwareData> { }
    public interface IBlockingRepository : IReadRepository<BlockingData> { }
    public interface IOpenTranactionRepository : IReadRepository<OpenTranactionData> { }

    public interface IVersionMap : IReadMapper<VersionData> { }
    public interface IHardwareMap : IReadMapper<HardwareData> { }
    public interface IBlockingMap : IReadMapper<BlockingData> { }
    public interface IOpenTranactionMap : IReadMapper<OpenTranactionData> { }
}
