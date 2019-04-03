namespace QIQO.Monitor.SQLServer.Data
{
    public class WaitStatsLogData : WaitStatsData
    {
        public int ServiceKey { get; set; }
        public long WaitTypeKey { get; set; }
    }
}