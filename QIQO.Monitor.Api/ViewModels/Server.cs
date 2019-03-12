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
        public Server(ServerData serverData, List<Service> services, List<Environment> environments) : this(serverData)
        {
            Services = services;
            Environments = environments;
        }
        public Server(int serverKey, string serverName)
        {
            ServerKey = serverKey;
            ServerName = serverName;
        }
        public Server(int serverKey, string serverName, List<Service> services, List<Environment> environments) : this(serverKey, serverName)
        {
            Services = services;
            Environments = environments;
        }
        public int ServerKey { get; }
        public string ServerName { get; }
        public List<Service> Services { get; } = new List<Service>();
        public List<Environment> Environments { get; } = new List<Environment>();
    }
}
