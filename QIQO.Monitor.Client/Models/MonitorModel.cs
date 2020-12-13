using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.Data;
using QIQO.Monitor.Domain;
using System.Collections.Generic;

namespace QIQO.Monitor.Client
{
    public class MonitorModel : IModel
    {
        public MonitorModel(MonitorData monitorData)
        {
            MonitorKey = monitorData.MonitorKey;
            MonitorName = monitorData.MonitorName;
            MonitorType = (MonitorType)monitorData.MonitorTypeKey;
            MonitorLevel = (MonitorLevel)monitorData.LevelKey;
            MonitorCategory = (MonitorCategories)monitorData.CategoryKey;
        }
        public MonitorModel(MonitorData monitorData, List<Query> queries) : this(monitorData) => Queries = queries;
        public MonitorModel(MonitorData monitorData, List<Query> queries, List<MonitorProperty> props) : this(monitorData, queries) => MonitorProperties = props;
        public int MonitorKey { get; }
        public string MonitorName { get; }
        public MonitorType MonitorType { get; }
        public MonitorLevel MonitorLevel { get; }
        public MonitorCategories MonitorCategory { get; }
        public List<Query> Queries { get; } = new List<Query>();
        public List<MonitorProperty> MonitorProperties { get; } = new List<MonitorProperty>();
        public MonitorResultPayload LastMonitorResult { get; } = new MonitorResultPayload { healthStatus = HealthStatus.Unknown };
    }
}
