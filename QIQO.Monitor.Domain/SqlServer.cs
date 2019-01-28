namespace QIQO.Monitor.Domain
{
    public class SqlServer : Server
    {
        public SqlServer(string serverName, string serverSource) : base(serverName, serverSource)
        {
            InstanceName = TryGetInstance(serverSource);
        }
        public string InstanceName { get; }
        private string TryGetInstance(string serverSource)
        {
            var nameArray = serverSource.Split("\\");
            return nameArray.Length > 1 ? nameArray[1] : string.Empty;
        }
    }
    public class Monitor
    {
        public string Name { get; }
    }
    public class SqlServerMonitor : Monitor
    {
        public string QueryText { get; }
    }
}
