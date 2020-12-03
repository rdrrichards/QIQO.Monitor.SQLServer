using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;

namespace QIQO.Monitor.Domain
{
    public class HealthResult : MonitorResult<Health>
    {
        public override IEnumerable<Health> results { get; set; } = new List<Health>();
        public override ResultType resultType { get; } = ResultType.Health;
    }
    public partial class Health : IModel
    {
        public Health(HealthStatus healthStatus) => HealthStatus = healthStatus;
        public HealthStatus HealthStatus { get; }
    }
    public enum HealthStatus
    {
        Healthly,
        Degraded,
        Unhealthy
    }
}
