namespace QIQO.Monitor.SQLServer.Data
{
    public class ServiceMonitorAttributeData : CommonData
    {
        public int ServiceKey { get; set; }
        public int MonitorKey { get; set; }
        public int AttributeTypeKey { get; set; }
        public string AttributeValue { get; set; }
    }
}