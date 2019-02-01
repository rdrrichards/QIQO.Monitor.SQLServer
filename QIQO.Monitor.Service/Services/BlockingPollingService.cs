using Microsoft.Extensions.Logging;
using QIQO.Monitor.Service.Polling;
using QIQO.Monitor.SQLServer.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QIQO.Monitor.Service.Services
{
    public interface IBlockingPollingService { }
    public class BlockingPollingService : PollingServiceBase, IBlockingPollingService
    {
        public BlockingPollingService(ILogger<BlockingPollingService> logger, IDbContextFactory dbContextFactory,
            IDataRepositoryFactory dataRepositoryFactory) : base(logger, dbContextFactory, dataRepositoryFactory)
        {
        }
        public string ServiceSource { get; set; }
        public void StartPolling(string serviceSource) => ServiceSource = serviceSource;
        public override void StartPolling()
        {
            var token = cancellationTokenSource.Token;
            var listener = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    _logger.LogInformation("Blocking Polling");
                    CreateContext(ServiceSource);
                    var repo = _dataRepositoryFactory.GetDataRepository<IBlockingRepository>();
                    try
                    {
                        var blockingData = repo.Get();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    
                    Thread.Sleep(PollingInterval);
                    if (token.IsCancellationRequested)
                        break;
                }

            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        ~BlockingPollingService()
        {
            StopPolling();
        }
    }
}
