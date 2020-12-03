using QIQO.Monitor.Core.Contracts;
using System;
using System.Collections.Generic;

namespace QIQO.Monitor.Domain
{
    public class OpenTransactionResult : MonitorResult<OpenTransaction>
    {
        public override IEnumerable<OpenTransaction> results { get; set; } = new List<OpenTransaction>();
        public override ResultType resultType { get; } = ResultType.OpenTransaction;
    }
    public partial class OpenTransaction : IModel
    {
        public OpenTransaction(int sessionId, string hostName, string loginName, long transactionID,
            string transactionName, DateTime transactionBegan, int databaseId, string databaseName)
        {
            this.sessionId = sessionId;
            this.hostName = hostName;
            this.loginName = loginName;
            this.transactionID = transactionID;
            this.transactionName = transactionName;
            this.transactionBegan = transactionBegan;
            this.databaseId = databaseId;
            this.databaseName = databaseName;
        }
        public int sessionId { get; }
        public string hostName { get; }
        public string loginName { get; }
        public long transactionID { get; }
        public string transactionName { get; }
        public DateTime transactionBegan { get; }
        public int databaseId { get; }
        public string databaseName { get; }
    }
}
