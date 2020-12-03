using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;

namespace QIQO.Monitor.Service
{
    public class MonitorModel : IModel
    {
        public int MonitorKey { get; set; }
        public string MonitorName { get; set; }
        public MonitorType MonitorType { get; set; }
        public MonitorLevel MonitorLevel { get; set; }
        public MonitorCategories MonitorCategory { get; set; }
        public List<Query> Queries { get; set; } = new List<Query>();
        public List<MonitorProperty> MonitorProperties { get; set; } = new List<MonitorProperty>();
    }
}
