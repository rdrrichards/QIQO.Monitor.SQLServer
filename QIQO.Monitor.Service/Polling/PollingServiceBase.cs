using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.Domain;
using QIQO.Monitor.SQLServer.Data;
using System.Collections.Generic;
using System.Threading;

namespace QIQO.Monitor.Service.Polling
{
    public interface IPollingService
    {
        void StartPolling();
        void StopPolling();
        void StartPolling(Server server, Service service);
    }
    public abstract class PollingServiceBase<T>
    {
        protected CancellationTokenSource cancellationTokenSource = null;
        protected readonly ILogger<PollingServiceBase<T>> _logger;
        protected readonly IDbContextFactory _dbContextFactory;
        protected readonly IDataRepositoryFactory _dataRepositoryFactory;
        protected readonly IHealthService _healthService;

        protected int PollingInterval { get; set; } = 30000;
        protected Service Service { get; set; }
        protected Server Server { get; set; }
        protected Monitor Monitor { get; set; }
        protected Query Query { get; set; }
        public PollingServiceBase(ILogger<PollingServiceBase<T>> logger, IDbContextFactory dbContextFactory,
            IDataRepositoryFactory dataRepositoryFactory, IHealthService healthService)
        {
            _logger = logger;
            _dbContextFactory = dbContextFactory;
            _dataRepositoryFactory = dataRepositoryFactory;
            _healthService = healthService;
            cancellationTokenSource = new CancellationTokenSource();
        }

        public virtual void StopPolling()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
        public virtual void StartPolling() { }
        public abstract PollingMonitorResult BuildMonitorResult(IEnumerable<T> blockingData);
        protected void CreateContext(string connectionString)
        {
            _dbContextFactory.Create(CreateConnectionString(connectionString));
        }
        private string CreateConnectionString(string serverSource)
        {
            return $"Data Source={serverSource};User ID=QIQOMonitorUser;Password=QIQOMonitorUser;Application Name=QIQOMonitorAPI";
        }
        protected void Assess(HealthStatus healthStatus) => _healthService.Assess(healthStatus, Server, Service, Monitor);
        protected void AssessHealthy() => Assess(HealthStatus.Healthly);
        protected void AssessUnhealthy() => Assess(HealthStatus.Unhealthy);
    }
}
