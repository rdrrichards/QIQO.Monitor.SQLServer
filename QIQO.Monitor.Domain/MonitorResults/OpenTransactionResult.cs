using QIQO.Monitor.Core.Contracts;
using System;
using System.Collections.Generic;

namespace QIQO.Monitor.Domain
{
    public class OpenTransactionResult : MonitorResult, IResultPayload<OpenTranaction>
    {
        IEnumerable<IModel> IResultPayload.Results => new List<OpenTranaction>();
    }
    public class OpenTranaction : IModel
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
