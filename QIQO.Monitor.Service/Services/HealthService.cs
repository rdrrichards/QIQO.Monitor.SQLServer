﻿using Microsoft.Extensions.Logging;
using QIQO.Monitor.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QIQO.Monitor.Service
{
    public interface IHealthService {
        void Assess(HealthStatus healthStatus, Server server, Service service, Monitor monitor);
    }
    public class HealthService : IHealthService
    {
        private readonly ILogger<HealthService> _logger;
        private readonly IHubClientService _hubClientService;
        private ConcurrentDictionary<string, Assessment> assessments = new ConcurrentDictionary<string, Assessment>();

        public HealthService(ILogger<HealthService> logger, IHubClientService hubClientService)
        {
            _logger = logger;
            _hubClientService = hubClientService;
        }
        public void Initialize(List<Server> servers) { }
        public async void Assess(HealthStatus healthStatus, Server server, Service service, Monitor monitor) {
            // _logger.LogInformation(healthStatus.ToString());

            // Really need to see if the prior health assessment was different than this one, and only send the notification out if they are
            if (await AssessmentChangedAsync(healthStatus, server, service, monitor))
            {
                // _logger.LogInformation($"***Assess: Hash: {server.ServerKey}_{service.ServiceKey}_{monitor.MonitorKey} new health update to {healthStatus}");
                var healthResult = new HealthResult();
                healthResult.Results.Add(new Health(healthStatus));
                await _hubClientService.SendResult(ResultType.Health, new PollingMonitorResult(server, service, healthResult));
            }
        }
        private async Task<bool> AssessmentChangedAsync(HealthStatus healthStatus, Server server, Service service, Monitor monitor)
        {
            return await Task.Run(() => {
                var newAssessment = new Assessment(server.ServerKey, service.ServiceKey, monitor.MonitorKey, healthStatus);
                var existAssessment = assessments.GetOrAdd(newAssessment.Hash, newAssessment);
                //_logger.LogInformation($"AssessmentChanged new: Hash: {newAssessment.Hash}; HealthStatus: {newAssessment.HealthStatus}; Date: {newAssessment.AssessmentDateTime}");
                //_logger.LogInformation($"AssessmentChanged exist: Hash: {existAssessment.Hash}; HealthStatus: {existAssessment.HealthStatus}; Date: {existAssessment.AssessmentDateTime}");
                if (newAssessment.AssessmentDateTime > existAssessment.AssessmentDateTime &&
                    newAssessment.HealthStatus != existAssessment.HealthStatus)
                {
                    assessments.AddOrUpdate(existAssessment.Hash, newAssessment, (k, v) => newAssessment);
                    return true;
                }
                else
                    return false;

            });
        }
    }
    public class Assessment
    {
        public Assessment(int serverKey, int serviceKey, int monitorKey, HealthStatus healthStatus)
        {
            ServerKey = serverKey;
            ServiceKey = serviceKey;
            MonitorKey = monitorKey;
            HealthStatus = healthStatus;
        }
        public int ServerKey { get; }
        public int ServiceKey { get; }
        public int MonitorKey { get; }
        public HealthStatus HealthStatus { get; }
        public string Hash => $"{ServerKey}_{ServiceKey}_{MonitorKey}";
        public DateTime AssessmentDateTime { get; } = DateTime.Now;
    }
}
