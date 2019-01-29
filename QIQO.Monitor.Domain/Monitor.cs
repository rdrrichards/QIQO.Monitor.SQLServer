using System.Collections.Generic;

namespace QIQO.Monitor.Domain
{
    public class Monitor
    {
        public Monitor() { }
        public Monitor(string name, MonitorType monitorType)
        {
            Name = name;
            MonitorType = MonitorType;
        }
        public string Name { get; } = string.Empty;
        public MonitorType MonitorType { get; } = MonitorType.Unknown;
        public IEnumerable<MonitorResult> MonitorResults { get; } = new List<MonitorResult>();
    }
}
