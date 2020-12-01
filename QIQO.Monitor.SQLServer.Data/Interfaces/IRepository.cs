using QIQO.Monitor.Core.Contracts;
using System;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    // Monitor results related interfaces
    public interface IVersionRepository : IReadRepository<VersionData> { }
    public interface IHardwareRepository : IReadRepository<HardwareData>
    {
        IEnumerable<HardwareData> Get(string queryText);
    }
    public interface IBlockingRepository : IReadRepository<BlockingData>
    {
        IEnumerable<BlockingData> Get(string queryText);
    }
    public interface IOpenTransactionRepository : IReadRepository<OpenTransactionData>
    {
        IEnumerable<OpenTransactionData> Get(string queryText);
    }

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
    public interface IWaitStatsLogRepository : IRepository<WaitStatsLogData>
    {
        IEnumerable<WaitStatsLogData> Get(int serviceKey);
        IEnumerable<WaitStatsLogData> Get(int serviceKey, int sampleCount);
        IEnumerable<WaitStatsLogData> Get(int serviceKey, DateTime startDate, DateTime endDate);
    }
}