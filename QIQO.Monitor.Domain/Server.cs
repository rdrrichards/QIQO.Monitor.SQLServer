using System;
using System.Collections.Generic;
using System.Text;

namespace QIQO.Monitor.Domain
{
    public class Server
    {
        public Server(string serverName, string serverSource)
        {
            ServerName = serverName;
            ServerSource = serverSource;
        }
        public string ServerName { get; }
        public string ServerSource { get; }
    }
}
