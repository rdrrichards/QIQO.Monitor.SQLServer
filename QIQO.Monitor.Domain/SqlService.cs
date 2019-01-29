using System.Collections.Generic;

namespace QIQO.Monitor.Domain
{
    public class SqlService : Service
    {
        public SqlService(string name) : base(name, ServiceType.SqlServer) { }
        public SqlService(string name, Server server) : this(name) => Server = server;
        public SqlService(string name, Server server, IEnumerable<SqlServerMonitor> monitors) : this(name, server) => Monitors = monitors;
        public Server Server { get; }
        public IEnumerable<SqlServerMonitor> Monitors { get; }
        public string InstanceName { get; }
    }
}
