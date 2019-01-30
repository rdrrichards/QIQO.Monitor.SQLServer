using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.Domain;
using QIQO.Monitor.SQLServer.Data;
using System;

namespace QIQO.Monitor.Api.Services
{
    public interface IOpenTranactionEntityService : IEntityService<OpenTranaction, OpenTranactionData> { }
    public class OpenTranactionEntityService : IOpenTranactionEntityService
    {
        public OpenTranaction Map(OpenTranactionData ent) => new OpenTranaction(ent.SessionId, ent.HostName, ent.LoginName,
                ent.TransactionID, ent.TransactionName, ent.TransactionBegan, ent.DatabaseId, ent.DatabaseName);

        public OpenTranactionData Map(OpenTranaction ent) => throw new NotImplementedException();
    }
}
