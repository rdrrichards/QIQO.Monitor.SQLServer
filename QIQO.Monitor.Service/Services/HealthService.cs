using Microsoft.Extensions.Logging;
using QIQO.Monitor.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QIQO.Monitor.Service
{
    public interface IHealthService {
        void Assess(HealthStatus healthStatus, Server server, Service service);
    }
    public class HealthService : IHealthService
    {
        private readonly ILogger<HealthService> _logger;
        private readonly IHubClientService _hubClientService;

        public HealthService(ILogger<HealthService> logger, IHubClientService hubClientService)
        {
            _logger = logger;
            _hubClientService = hubClientService;
        }
        public void Initialize(List<Server> servers) { }
        public void Assess(HealthStatus healthStatus, Server server, Service service) {
            // _logger.LogInformation(healthStatus.ToString());

            // Really need to see if the prior health assessment was different than this one, and only send the notification out if they are

            // if (healthStatus == HealthStatus.Unhealthy)
            var healthResult = new HealthResult();
            healthResult.Results.Add(new Health(healthStatus));
            _hubClientService.SendResult(ResultType.Health, new PollingMonitorResult(server, service, healthResult));
        }
    }
}
