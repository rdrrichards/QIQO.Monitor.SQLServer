using Microsoft.Extensions.Logging;
using QIQO.Monitor.SQLServer.Data;
using System.Threading;

namespace QIQO.Monitor.Service.Polling
{
    public interface IPollingService
    {
        void StartPolling();
        void StopPolling();
    }
    public abstract class PollingServiceBase
    {
        protected CancellationTokenSource cancellationTokenSource = null;
        protected readonly ILogger<PollingServiceBase> _logger;
        protected readonly IDbContextFactory _dbContextFactory;
        protected readonly IDataRepositoryFactory _dataRepositoryFactory;

        protected int PollingInterval { get; set; } = 30000;
        public PollingServiceBase(ILogger<PollingServiceBase> logger, IDbContextFactory dbContextFactory,
            IDataRepositoryFactory dataRepositoryFactory)
        {
            _logger = logger;
            _dbContextFactory = dbContextFactory;
            _dataRepositoryFactory = dataRepositoryFactory;
            cancellationTokenSource = new CancellationTokenSource();
        }

        public virtual void StopPolling()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
        public virtual void StartPolling() { }
        protected void CreateContext(string connectionString)
        {
            _dbContextFactory.Create(CreateConnectionString(connectionString));
        }
        private string CreateConnectionString(string serverSource)
        {
            return $"Data Source={serverSource};User ID=QIQOMonitorUser;Password=QIQOMonitorUser;Application Name=QIQOMonitorAPI";
        }
    }
}
