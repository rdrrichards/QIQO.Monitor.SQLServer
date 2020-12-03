using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;

namespace QIQO.Monitor.Service
{
    public class Server : IModel
    {
        public int ServerKey { get; set; }
        public string ServerName { get; set; }
        public List<Service> Services { get; set; } = new List<Service>();
        public List<Environment> Environments { get; set; } = new List<Environment>();
    }

}
