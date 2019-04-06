namespace QIQO.Monitor.SQLServer.Data
{
    public class ServiceMonitorData : CommonData
    {
        public int ServiceKey { get; set; }
        public int MonitorKey { get; set; }
        public bool Enabled { get; set; }
    }
}