using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public interface ICoreCacheService
    {
        IEnumerable<ServerData> GetServers();
        ServerData GetServer(int serverKey);
        IEnumerable<QueryData> GetQueries();
        IEnumerable<QueryData> GetQueries(int monitorKey);
        QueryData GetQuery(string name, int level);
        QueryData GetQuery(int id);

        IEnumerable<ServiceData> GetServices();
        ServiceData GetService(int serviceKey);
        IEnumerable<ServiceData> GetServices(int serverKey);
        IEnumerable<MonitorData> GetMonitors();
        IEnumerable<MonitorData> GetMonitors(int serviceType);
        IEnumerable<MonitorQueryData> GetMonitorQueries();
    }
}
