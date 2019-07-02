using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Api
{
    public interface IServerEntityService : IEntityService<Server, ServerData> { }
    public interface IServiceEntityService : IEntityService<Service, ServiceData> { }
    public interface IMonitorEntityService : IEntityService<Monitor, MonitorData> { }
    public interface IQueryEntityService : IEntityService<Query, QueryData> { }
    public interface IEnvironmentEntityService : IEntityService<Environment, EnvironmentData> { }
    public interface IMonitorPropertiesEntityService
    {
        MonitorProperty Map(ServiceMonitorAttributeData serviceMonitorAttributeData,
            AttributeTypeData attributeTypeData, AttributeDataTypeData attributeDataTypeData);
    }
}
