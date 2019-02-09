using QIQO.Monitor.Domain;
using System;

namespace QIQO.Monitor.Service
{
    public class PollingMonitorResult
    {
        public PollingMonitorResult(Server server, Service service, IMonitorResult monitorResult)
        {
            Server = server;
            Service = service;
            MonitorResult = monitorResult;
        }
        public PollingMonitorResult(Server server, Service service, IMonitorResult monitorResult, Exception exception) :
            this(server, service, monitorResult)
        {
            if (exception != null)
            {
                HasError = true;
                Exception = exception;
            }
        }
        public PollingMonitorResult(Server server, Service service, Exception exception) : this(server, service, null, exception) { }
        public IMonitorResult MonitorResult { get; }
        public bool HasError { get; } = false;
        public Exception Exception { get; } = null;
        public Server Server { get; }
        public Service Service { get; }
    }
}
