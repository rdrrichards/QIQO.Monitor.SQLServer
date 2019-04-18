using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public interface ICoreCacheService
    {
        void RefreshCache();
        IEnumerable<ServerData> GetServers();
        ServerData GetServer(int serverKey);
        IEnumerable<QueryData> GetQueries();
        IEnumerable<QueryData> GetQueries(int monitorKey);
        QueryData GetQuery(string name);
        QueryData GetQuery(int id);

        IEnumerable<ServiceData> GetServices();
        ServiceData GetService(int serviceKey);
        IEnumerable<ServiceData> GetServices(int serverKey);
        IEnumerable<MonitorData> GetMonitors();
        IEnumerable<MonitorData> GetMonitors(int serviceType);
        IEnumerable<MonitorQueryData> GetMonitorQueries();

        IEnumerable<EnvironmentData> GetEnviroments();
        IEnumerable<EnvironmentServiceData> GetEnvironmentServices();
        IEnumerable<EnvironmentData> GetServerEnvironments(int serverKey);
        IEnumerable<EnvironmentData> GetServiceEnvironments(int serviceKey);

        IEnumerable<ServiceMonitorData> GetServiceMonitors();
        IEnumerable<MonitorData> GetServiceMonitors(int serviceKey);
        IEnumerable<MonitorData> GetActiveServiceMonitors(int serviceKey);
        ServiceMonitorData GetServiceMonitors(int serviceKey, int monitorKey);
        IEnumerable<CategoryData> GetCategories();
        IEnumerable<AttributeTypeData> GetAttributeTypes();
        IEnumerable<AttributeDataTypeData> GetAttributeDataTypes();
        IEnumerable<ServiceMonitorAttributeData> GetServiceMonitorAttributes();
        IEnumerable<ServiceMonitorAttributeData> GetServiceMonitorAttributes(int serviceKey, int monitorKey);
    }
}
