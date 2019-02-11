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
            MonitorCategory = (MonitorCategory)monitorData.CategoryKey;
        }
        public Monitor(MonitorData monitorData, List<Query> queries) : this(monitorData) => Queries = queries;
        public int MonitorKey { get; }
        public string MonitorName { get; }
        public MonitorType MonitorType { get; }
        public MonitorLevel MonitorLevel { get; }
        public MonitorCategory MonitorCategory { get; }
        public List<Query> Queries { get; } = new List<Query>();
    }
}
