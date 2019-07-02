namespace QIQO.Monitor.SQLServer.Data
{
    public class MonitorData : CommonData
    {
        public int MonitorKey { get; set; }
        public int MonitorTypeKey { get; set; }
        public string MonitorName { get; set; }
        public int LevelKey { get; set; }
        public int CategoryKey { get; set; }
    }
}