using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using QIQO.Monitor.Domain;

namespace QIQO.Monitor.Service
{
    public abstract class MonitorServiceBase<T>
    {
        protected CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public virtual void StopPolling()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
        public abstract Task StartPolling(int inverval = 10000);
        protected string CreateConnectionString(string serverSource)
        {
            return $"Data Source={serverSource};User ID=QIQOMonitorUser;Password=QIQOMonitorUser;Application Name=QIQOMonitorService";
        }
        public abstract MonitorResultPayload BuildMonitorResult(IEnumerable<T> data);
        public abstract Task Assess(HealthStatus healthStatus, object result); // => _healthService.Assess(healthStatus, Server, Service, Monitor);
        protected virtual Task AssessHealthy(object result) => Assess(HealthStatus.Healthly, result);
        protected virtual Task AssessUnhealthy(object result) => Assess(HealthStatus.Unhealthy, result);
        protected virtual Task AssessDegraded(object result) => Assess(HealthStatus.Degraded, result);
    }
}
