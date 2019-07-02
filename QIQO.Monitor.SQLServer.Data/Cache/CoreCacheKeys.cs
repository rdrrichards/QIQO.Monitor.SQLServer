namespace QIQO.Monitor.SQLServer.Data
{
    public static class CoreCacheKeys
    {
        public static string Servers { get { return "_Servers"; } }
        public static string Services { get { return "_Services"; } }
        public static string Monitors { get { return "_Monitors"; } }
        public static string Queries { get { return "_Queries"; } }
        public static string MonitorQueries { get { return "_MonitorQueries"; } }
        public static string Environments { get { return "_Environments"; } }
        public static string EnvironmentServices { get { return "_EnvironmentServices"; } }
        public static string EnvironmentServers { get { return "_EnvironmentServers"; } }
        public static string ServiceMonitors { get { return "_ServiceMonitors"; } }
        public static string MonitorCategories { get { return "_MonitorCategories"; } }
        public static string AttributeTypes { get { return "_AttributeTypes"; } }
        public static string AttributeDataTypes { get { return "_AttributeDataTypes"; } }
        public static string ServiceMonitorAttributes { get { return "_ServiceMonitorAttributes"; } }
    }
}
