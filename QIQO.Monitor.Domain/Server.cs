using System.Collections.Generic;

namespace QIQO.Monitor.Domain
{
    public class Server
    {
        public Server(string name) => Name = name;
        public string Name { get; }
        public IEnumerable<Monitor> Monitors { get; }
    }
}
