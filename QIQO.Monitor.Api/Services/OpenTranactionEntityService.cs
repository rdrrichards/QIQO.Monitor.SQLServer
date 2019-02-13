using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.Domain;
using QIQO.Monitor.SQLServer.Data;
using System;

namespace QIQO.Monitor.Api.Services
{
    public interface IOpenTranactionEntityService : IEntityService<OpenTransaction, OpenTransactionData> { }
    public class OpenTranactionEntityService : IOpenTranactionEntityService
    {
        public OpenTransaction Map(OpenTransactionData ent) => new OpenTransaction(ent.SessionId, ent.HostName, ent.LoginName,
                ent.TransactionID, ent.TransactionName, ent.TransactionBegan, ent.DatabaseId, ent.DatabaseName);

        public OpenTransactionData Map(OpenTransaction ent) => throw new NotImplementedException();
    }
}
