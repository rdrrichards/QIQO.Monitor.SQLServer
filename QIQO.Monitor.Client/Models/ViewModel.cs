namespace QIQO.Monitor.Client
{
    public class ServerAdd
    {
        public string ServerName { get; set; }
        public int[] Services { get; set; }
        public int[] Environments { get; set; }
    }
    public class ServiceAdd
    {
        public string ServiceName { get; set; }
        public string InstanceName { get; set; }
        public string ServiceSource { get; set; }
        public int ServiceTypeKey { get; set; }
        public int ServerKey { get; set; }
        public int[] Environments { get; set; }
        public int[] Monitors { get; set; }
    }
    public class EnvironmentAdd
    {
        public string EnvironmentName { get; set; }
    }
    public class MonitorAdd
    {
        public string MonitorName { get; set; }
        public MonitorType MonitorType { get; set; }
        public MonitorLevel MonitorLevel { get; set; }
        public MonitorCategories MonitorCategory { get; set; }
    }
    public class QueryAdd
    {
        public string Name { get; set; }
        public string QueryText { get; set; }
    }
    public class ServerUpdate
    {
        public int ServerKey { get; set; }
        public string ServerName { get; set; }
        public int[] Services { get; set; }
        public int[] Environments { get; set; }
    }
    public class ServiceUpdate
    {
        public int ServiceKey { get; set; }
        public string ServiceName { get; set; }
        public string InstanceName { get; set; }
        public string ServiceSource { get; set; }
        public int ServiceTypeKey { get; set; }
        public int ServerKey { get; set; }
        public int[] Environments { get; set; }
        public int[] Monitors { get; set; }
    }
    public class EnvironmentUpdate
    {
        public int EnvironmentKey { get; set; }
        public string EnvironmentName { get; set; }
    }
    public class MonitorUpdate
    {
        public int MonitorKey { get; set; }
        public string MonitorName { get; set; }
        public MonitorType MonitorType { get; set; }
        public MonitorLevel MonitorLevel { get; set; }
        public MonitorCategories MonitorCategory { get; set; }
    }
    public class QueryUpdate
    {
        public int QueryKey { get; set; }
        public string Name { get; set; }
        public string QueryText { get; set; }
    }
}
