using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public interface ICoreCacheService
    {
        IEnumerable<ServerData> GetServers();
        ServerData GetServer(int serverKey);
        QueryData GetQuery(string name, int level);
    }
}
