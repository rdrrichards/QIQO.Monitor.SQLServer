using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;

namespace QIQO.Monitor.Domain
{
    public class HealthResult : MonitorResult<Health>
    {
        public override List<Health> Results { get; } = new List<Health>();
    }
    public partial class Health : IModel
    {
        public Health(HealthStatus healthStatus) => HealthStatus = healthStatus;
        public HealthStatus HealthStatus { get; }
    }
    public enum HealthStatus
    {
        Healthly,
        Unhealthy
    }
}
