namespace QIQO.Monitor.SQLServer.Data
{
    public class QueryHistoryData : CommonData
    {
        public int QueryHistoryKey { get; set; }
        public int ServiceKey { get; set; }
        public int MonitorKey { get; set; }
        public int QueryKey { get; set; }
        public string ResultText { get; set; }
    }
}