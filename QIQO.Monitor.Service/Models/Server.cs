using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Service
{
    public class Server : IModel
    {
        public int ServerKey { get; set; }
        public string ServerName { get; set; }
        public IEnumerable<Service> Services { get; set; } = Enumerable.Empty<Service>();
        public IEnumerable<Environment> Environments { get; set; } = Enumerable.Empty<Environment>();
    }

}
