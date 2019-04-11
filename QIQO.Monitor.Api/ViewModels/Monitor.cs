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
        public Monitor(MonitorData monitorData, List<Query> queries, bool enabled) : this(monitorData, queries) => Enabled = enabled;
        public int MonitorKey { get; }
        public string MonitorName { get; }
        public MonitorType MonitorType { get; }
        public MonitorLevel MonitorLevel { get; }
        public MonitorCategories MonitorCategory { get; }
        public bool Enabled { get; } = true;
        public List<Query> Queries { get; } = new List<Query>();
    }
}
