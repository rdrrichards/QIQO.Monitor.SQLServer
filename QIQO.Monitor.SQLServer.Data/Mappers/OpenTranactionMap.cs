using QIQO.Monitor.Core;
using System;
using System.Data;

namespace QIQO.Monitor.SQLServer.Data
{
    public class OpenTranactionMap : MapperBase, IOpenTranactionMap
    {
        public OpenTransactionData Map(IDataReader record)
        {
            try
            {
                return new OpenTransactionData()
                {
                    SessionId = NullCheck<int>(record["SessionId"]),
                    HostName = NullCheck<string>(record["HostName"]),
                    LoginName = NullCheck<string>(record["LoginName"]),
                    TransactionID = NullCheck<long>(record["TransactionID"]),
                    TransactionName = NullCheck<string>(record["TransactionName"]),
                    TransactionBegan = NullCheck<DateTime>(record["TransactionBegan"]),
                    DatabaseId = NullCheck<int>(record["DatabaseId"]),
                    DatabaseName = NullCheck<string>(record["DatabaseName"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"OpenTranactionMap Exception occured: {ex.Message}", ex);
            }
        }
    }
}
