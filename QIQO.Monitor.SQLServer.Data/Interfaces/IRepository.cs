using QIQO.Monitor.Core.Contracts;
using System;
using System.Collections.Generic;

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
    public interface IEnvironmentRepository : IRepository<EnvironmentData> { }
    public interface IEnvironmentServerRepository : IRepository<EnvironmentServerData> { }
    public interface IEnvironmentServiceRepository : IRepository<EnvironmentServiceData> { }


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
    public interface IHardwareRepository : IReadRepository<HardwareData>
    {
        IEnumerable<HardwareData> Get(string queryText);
    }
    public interface IBlockingRepository : IReadRepository<BlockingData> {
        IEnumerable<BlockingData> Get(string queryText);
    }
    public interface IOpenTransactionRepository : IReadRepository<OpenTransactionData> {
        IEnumerable<OpenTransactionData> Get(string queryText);
    }
    public interface IEnvironmentMap : IMapper<EnvironmentData> { }
    public interface IEnvironmentServerMap : IMapper<EnvironmentServerData> { }
    public interface IEnvironmentServiceMap : IMapper<EnvironmentServiceData> { }

    public interface IVersionMap : IReadMapper<VersionData> { }
    public interface IHardwareMap : IReadMapper<HardwareData> { }
    public interface IBlockingMap : IReadMapper<BlockingData> { }
    public interface IOpenTranactionMap : IReadMapper<OpenTransactionData> { }
    public interface IWaitStatsMap : IReadMapper<WaitStatsData> { }
    public interface IWaitStatsRepository : IReadRepository<WaitStatsData>
    {
        IEnumerable<WaitStatsData> Get(string queryText);
    }

    public interface IWaitStatsLogMap : IMapper<WaitStatsLogData> { }
    public interface IWaitStatsLogRepository : IRepository<WaitStatsLogData> {
        IEnumerable<WaitStatsLogData> Get(int serviceKey);
        IEnumerable<WaitStatsLogData> Get(int serviceKey, int sampleCount);
        IEnumerable<WaitStatsLogData> Get(int serviceKey, DateTime startDate, DateTime endDate);
    }
}
