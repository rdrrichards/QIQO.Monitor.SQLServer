namespace QIQO.Monitor.SQLServer.Data
{
    public class ServiceData : CommonData
    {
        public int ServiceKey { get; set; }
        public int ServiceTypeKey { get; set; }
        public int ServerKey { get; set; }
        public string ServiceName { get; set; }
        public string InstanceName { get; set; }
    }
}