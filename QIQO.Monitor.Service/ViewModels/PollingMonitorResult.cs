using QIQO.Monitor.Domain;
using System;

namespace QIQO.Monitor.Service
{
    public class PollingMonitorResult
    {
        public PollingMonitorResult(Server server, Service service, Monitor monitor, IMonitorResult monitorResult)
        {
            Server = server;
            Service = service;
            Monitor = monitor;
            MonitorResult = monitorResult;
        }
        public PollingMonitorResult(Server server, Service service, Monitor monitor, IMonitorResult monitorResult, Exception exception) :
            this(server, service, monitor, monitorResult)
        {
            if (exception != null)
            {
                HasError = true;
                Exception = exception;
            }
        }
        public PollingMonitorResult(Server server, Service service, Monitor monitor, Exception exception) :
            this(server, service, monitor, null, exception) { }
        public IMonitorResult MonitorResult { get; }
        public bool HasError { get; } = false;
        public Exception Exception { get; } = null;
        public Server Server { get; }
        public Service Service { get; }
        public Monitor Monitor { get; }
        public int ServerKey => Server.ServerKey;
        public int ServiceKey => Service.ServiceKey;
        public int MonitorKey => Monitor.MonitorKey;
    }
}
