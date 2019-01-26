using QIQO.Monitor.Core;
using System;
using System.Data;

namespace QIQO.Monitor.SQLServer.Data
{
    public class HardwareMap : MapperBase, IHardwareMap
    {
        public HardwareData Map(IDataReader record)
        {
            try
            {
                return new HardwareData()
                {
                    LogicalCPUCount = NullCheck<int>(record["LogicalCPUCount"]),
                    SchedulerCount = NullCheck<int>(record["SchedulerCount"]),
                    PhysicalCoreCount = NullCheck<int>(record["PhysicalCoreCount"]),
                    SocketCount = NullCheck<int>(record["SocketCount"]),
                    CoresPerSocket = NullCheck<int>(record["CoresPerSocket"]),
                    NUMANodeCount = NullCheck<int>(record["NUMANodeCount"]),
                    PhysicalMemoryMB = NullCheck<long>(record["PhysicalMemoryMB"]),
                    MaxWorkersCount = NullCheck<int>(record["MaxWorkersCount"]),
                    AffinityType = NullCheck<string>(record["AffinityType"]),
                    SQLServerStartTime = NullCheck<DateTime>(record["SQLServerStartTime"]),
                    VirtualMachineType = NullCheck<string>(record["VirtualMachineType"]),
                    SoftNUMAConfiguration = NullCheck<string>(record["SoftNUMAConfiguration"]),
                    SQLMemoryModelDesc = NullCheck<string>(record["SQLMemoryModelDesc"]),
                    ProcessPhysicalAffinity = NullCheck<string>(record["ProcessPhysicalAffinity"]),
                };
            }
            catch (Exception ex)
            {
                throw new MapException($"HardwareMap Exception occured: {ex.Message}", ex);
            }
        }
    }
}
