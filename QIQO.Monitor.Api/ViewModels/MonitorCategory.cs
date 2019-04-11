namespace QIQO.Monitor.Api
{
    public enum MonitorCategories
    {
        Version = 1,
        SQLServerHardware,
        DetectBlocking,
        OpenTranactions,
        WaitStatistics
    }
    public class MonitorCategory
    {
        public int CategoryKey { get; set; }
        public string CategoryName { get; set; }
    }
}
