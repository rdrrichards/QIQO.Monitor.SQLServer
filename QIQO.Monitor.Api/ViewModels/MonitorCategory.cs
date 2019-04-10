namespace QIQO.Monitor.Api
{
    public enum MonitorCategory
    {
        Version = 1,
        SQLServerHardware,
        DetectBlocking,
        OpenTranactions,
        WaitStatistics
    }
    public class MonitorCategoryVM
    {
        public int CategoryKey { get; set; }
        public string CategoryName { get; set; }
    }
}
