using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.SQLServer.Data;
using System.Collections.Generic;

namespace QIQO.Monitor.Api
{
    public class Server : IModel
    {
        public Server(ServerData serverData)
        {
            ServerKey = serverData.ServerKey;
            ServerName = serverData.ServerName;
        }
        public Server(ServerData serverData, List<Service> services) : this(serverData) => Services = services;
        public Server(int serverKey, string serverName)
        {
            ServerKey = serverKey;
            ServerName = serverName;
        }
        public Server(int serverKey, string serverName, List<Service> services) : this(serverKey, serverName) => Services = services;
        public int ServerKey { get; }
        public string ServerName { get; }
        public List<Service> Services { get; } = new List<Service>();
    }
}
