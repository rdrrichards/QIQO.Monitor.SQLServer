using System;

namespace QIQO.Monitor.Domain
{
    public class MonitorResultPayload
    {
        public MonitorResultPayload() { }
        public MonitorResultPayload(IMonitorResult monitorResult) => this.monitorResult = monitorResult;
        public MonitorResultPayload(IMonitorResult monitorResult, Exception exception) : this(monitorResult)
        {
            if (exception != null)
            {
                hasError = true;
                this.exception = exception;
            }
        }
        public MonitorResultPayload(Exception exception) : this(null, exception) { }
        public IMonitorResult monitorResult { get; set; }
        public bool hasError { get; set; } = false;
        public Exception exception { get; set; } = null;
        public HealthStatus healthStatus { get; set; } = HealthStatus.Healthly;
        public string serviceName { get; set; }
        public string monitorName { get; set; }
    }
}
