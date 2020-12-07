using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Service
{
    public class MonitorModel : IModel
    {
        public int MonitorKey { get; set; }
        public string MonitorName { get; set; }
        public MonitorType MonitorType { get; set; }
        public MonitorLevel MonitorLevel { get; set; }
        public MonitorCategories MonitorCategory { get; set; }
        public IEnumerable<Query> Queries { get; set; } = Enumerable.Empty<Query>();
        public IEnumerable<MonitorProperty> MonitorProperties { get; set; } = Enumerable.Empty<MonitorProperty>();
    }
}
