using Microsoft.Extensions.Logging;
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
    public abstract class PollingServiceBase<T> // : IPollingService
    {
        protected CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        protected readonly ILogger<PollingServiceBase<T>> _logger;
        protected readonly IDbContextFactory _dbContextFactory;
        protected readonly IDataRepositoryFactory _dataRepositoryFactory;
        protected readonly IHealthService _healthService;

        protected int PollingInterval { get; set; } = 120000;
        protected Service Service { get; set; } = new Service(new ServiceData { InstanceName = string.Empty, ServerKey = 0, ServiceKey = 0, ServiceName = string.Empty, ServiceSource = string.Empty, ServiceTypeKey = 0 });
        protected Server Server { get; set; } = new Server(new ServerData { ServerKey = 0, ServerName = string.Empty });
        protected Monitor Monitor { get; set; } = new Monitor(new MonitorData { CategoryKey = 0, LevelKey = 0, MonitorKey = 0, MonitorName = string.Empty, MonitorTypeKey = 0 });
        protected Query Query { get; set; } = new Query(new QueryData { Name = string.Empty, QueryKey = 0, QueryText = string.Empty });
        public PollingServiceBase(ILogger<PollingServiceBase<T>> logger, IDbContextFactory dbContextFactory,
            IDataRepositoryFactory dataRepositoryFactory, IHealthService healthService)
        {
            _logger = logger;
            _dbContextFactory = dbContextFactory;
            _dataRepositoryFactory = dataRepositoryFactory;
            _healthService = healthService;
        }

        public virtual void StopPolling()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
        public abstract void StartPolling();
        public abstract void StartPolling(Server server, Service service);
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
