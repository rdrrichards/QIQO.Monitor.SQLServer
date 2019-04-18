using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.SQLServer.Data;
using System.Collections.Generic;

namespace QIQO.Monitor.Api
{
    public class Monitor : IModel
    {
        public Monitor(MonitorData monitorData)
        {
            MonitorKey = monitorData.MonitorKey;
            MonitorName = monitorData.MonitorName;
            MonitorType = (MonitorType)monitorData.MonitorTypeKey;
            MonitorLevel = (MonitorLevel)monitorData.LevelKey;
            MonitorCategory = (MonitorCategories)monitorData.CategoryKey;
        }
        public Monitor(MonitorData monitorData, List<Query> queries) : this(monitorData) => Queries = queries;
        public Monitor(MonitorData monitorData, List<Query> queries, List<MonitorProperty> props) : this(monitorData, queries) => MonitorProperties = props;
        public int MonitorKey { get; }
        public string MonitorName { get; }
        public MonitorType MonitorType { get; }
        public MonitorLevel MonitorLevel { get; }
        public MonitorCategories MonitorCategory { get; }
        public List<Query> Queries { get; } = new List<Query>();
        public List<MonitorProperty> MonitorProperties { get; } = new List<MonitorProperty>();
    }
}
