using QIQO.Monitor.SQLServer.Data;
using System;

namespace QIQO.Monitor.Api
{
    public class MonitorEntityService : IMonitorEntityService
    {
        public Monitor Map(MonitorData ent) => new Monitor(ent);

        public MonitorData Map(Monitor ent) => throw new NotImplementedException();
    }
}
