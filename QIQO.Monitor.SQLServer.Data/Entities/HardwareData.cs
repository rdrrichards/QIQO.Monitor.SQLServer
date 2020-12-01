using QIQO.Monitor.Core.Contracts;
using System;

namespace QIQO.Monitor.SQLServer.Data
{
    public class HardwareData : IEntity
    {
        public int LogicalCPUCount { get; set; }
        public int SchedulerCount { get; set; }
        public int PhysicalCoreCount { get; set; }
        public int SocketCount { get; set; }
        public int CoresPerSocket { get; set; }
        public int NUMANodeCount { get; set; }
        public long PhysicalMemoryMB { get; set; }
        public int MaxWorkersCount { get; set; }
        public string AffinityType { get; set; }
        public DateTime SQLServerStartTime { get; set; }
        public string VirtualMachineType { get; set; }
        public string SoftNUMAConfiguration { get; set; }
        public string SQLMemoryModelDesc { get; set; }
        public string ProcessPhysicalAffinity { get; set; }
    }
}