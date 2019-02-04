using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Service
{
    public class Monitor : IModel
    {
        public Monitor(MonitorData monitorData)
        {
            MonitorKey = monitorData.MonitorKey;
            MonitorName = monitorData.MonitorName;
            MonitorType = (MonitorType)monitorData.MonitorTypeKey;
        }
        public int MonitorKey { get; }
        public string MonitorName { get; }
        public MonitorType MonitorType { get; }
    }
}
