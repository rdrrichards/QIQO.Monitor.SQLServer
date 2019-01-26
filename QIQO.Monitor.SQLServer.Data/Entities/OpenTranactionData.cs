using System;

namespace QIQO.Monitor.SQLServer.Data
{
    public class OpenTranactionData : CommonData
    {
        public int SessionId { get; set; }
        public string HostName { get; set; }
        public string LoginName { get; set; }
        public long TransactionID { get; set; }
        public string TransactionName { get; set; }
        public DateTime TransactionBegan { get; set; }
        public int DatabaseId { get; set; }
        public string DatabaseName { get; set; }
    }
}