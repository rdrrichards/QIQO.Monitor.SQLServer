﻿using QIQO.Monitor.Core.Contracts;
using System;
using System.Collections.Generic;

namespace QIQO.Monitor.Domain
{
    public class OpenTransactionResult : MonitorResult<OpenTransaction>
    {
        public override List<OpenTransaction> Results { get; } = new List<OpenTransaction>();
    }
    public partial class OpenTransaction : IModel
    {
        public OpenTransaction(int sessionId, string hostName, string loginName, long transactionID,
            string transactionName, DateTime transactionBegan, int databaseId, string databaseName)
        {
            SessionId = sessionId;
            HostName = hostName;
            LoginName = loginName;
            TransactionID = transactionID;
            TransactionName = transactionName;
            TransactionBegan = transactionBegan;
            DatabaseId = databaseId;
            DatabaseName = databaseName;
        }
        public int SessionId { get; }
        public string HostName { get; }
        public string LoginName { get; }
        public long TransactionID { get; }
        public string TransactionName { get; }
        public DateTime TransactionBegan { get; }
        public int DatabaseId { get; }
        public string DatabaseName { get; }
    }
}
