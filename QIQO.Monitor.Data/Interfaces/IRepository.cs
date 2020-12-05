using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;

namespace QIQO.Monitor.Data
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
    public interface IServiceMonitorRepository : IRepository<ServiceMonitorData>
    {
        IEnumerable<ServiceMonitorData> GetAll(int service_key, int monitor_key);
    }
    public interface IServiceMonitorAttributeRepository : IRepository<ServiceMonitorAttributeData>
    {
        IEnumerable<ServiceMonitorAttributeData> GetAll(int service_key, int monitor_key);
    }
    public interface IAttributeTypeRepository : IRepository<AttributeTypeData> { }
    public interface IAttributeDataTypeRepository : IRepository<AttributeDataTypeData> { }


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
    public interface IServiceMonitorMap : IMapper<ServiceMonitorData> { }
    public interface IServiceMonitorAttributeMap : IMapper<ServiceMonitorAttributeData> { }
    public interface IAttributeTypeMap : IMapper<AttributeTypeData> { }
    public interface IAttributeDataTypeMap : IMapper<AttributeDataTypeData> { }

    // Monitor results related interfaces

    public interface IEnvironmentMap : IMapper<EnvironmentData> { }
    public interface IEnvironmentServerMap : IMapper<EnvironmentServerData> { }
    public interface IEnvironmentServiceMap : IMapper<EnvironmentServiceData> { }

}
