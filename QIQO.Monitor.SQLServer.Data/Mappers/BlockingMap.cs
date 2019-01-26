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
                    LockType = NullCheck<string>(record["SchedulerCount"]),
                    Database = NullCheck<string>(record["SchedulerCount"]),
                    BlockObject = NullCheck<int>(record["SchedulerCount"]),
                    LockRequest = NullCheck<string>(record["SchedulerCount"]),
                    WaiterSid = NullCheck<int>(record["SchedulerCount"]),
                    WaitTime = NullCheck<int>(record["SchedulerCount"]),
                    WaiterBatch = NullCheck<string>(record["SchedulerCount"]),
                    WaiterStatement = NullCheck<string>(record["SchedulerCount"]),
                    BlockerSid = NullCheck<int>(record["SchedulerCount"]),
                    BlockerBatch = NullCheck<string>(record["SchedulerCount"])
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"BlockingMap Exception occured: {ex.Message}", ex);
            }
        }
    }
}
