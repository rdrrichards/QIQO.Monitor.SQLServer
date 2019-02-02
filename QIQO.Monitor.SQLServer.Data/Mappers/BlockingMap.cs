using QIQO.Monitor.Core;
using System;
using System.Data;

namespace QIQO.Monitor.SQLServer.Data
{
    public class BlockingMap : MapperBase, IBlockingMap
    {
        public BlockingData Map(IDataReader record)
        {
            try
            {
                return new BlockingData()
                {
                    LockType = NullCheck<string>(record["LockType"]),
                    Database = NullCheck<string>(record["Database"]),
                    BlockObject = NullCheck<long>(record["BlockObject"]),
                    LockRequest = NullCheck<string>(record["LockRequest"]),
                    WaiterSid = NullCheck<int>(record["WaiterSid"]),
                    WaitTime = NullCheck<long>(record["WaitTime"]),
                    WaiterBatch = NullCheck<string>(record["WaiterBatch"]),
                    WaiterStatement = NullCheck<string>(record["WaiterStatement"]),
                    BlockerSid = NullCheck<short>(record["BlockerSid"]),
                    BlockerBatch = NullCheck<string>(record["BlockerBatch"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"BlockingMap Exception occured: {ex.Message}", ex);
            }
        }
    }
}
