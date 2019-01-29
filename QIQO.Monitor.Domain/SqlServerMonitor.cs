namespace QIQO.Monitor.Domain
{
    public class SqlServerMonitor : Monitor
    {
        public SqlServerMonitor() { }
        public SqlServerMonitor(string name) : base (name, MonitorType.SqlServer) { }
        public SqlServerMonitor(string name, SqlMonitorLevel sqlMonitorLevel) : this(name) => MonitorLevel = sqlMonitorLevel;
        public SqlMonitorLevel MonitorLevel { get; } = SqlMonitorLevel.Instance;
        public string QueryText { get; } = string.Empty;
    }
}
