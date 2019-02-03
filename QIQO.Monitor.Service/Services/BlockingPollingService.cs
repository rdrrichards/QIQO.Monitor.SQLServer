using Microsoft.Extensions.Logging;
using QIQO.Monitor.Domain;
using QIQO.Monitor.Service.Polling;
using QIQO.Monitor.SQLServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QIQO.Monitor.Service.Services
{
    public interface IBlockingPollingService : IPollingService {
        void StartPolling(string serviceSource);
    }
    public class BlockingPollingService : PollingServiceBase, IBlockingPollingService
    {
        private readonly IHubClientService _hubClientService;

        public BlockingPollingService(ILogger<BlockingPollingService> logger, IDbContextFactory dbContextFactory,
            IDataRepositoryFactory dataRepositoryFactory, IHubClientService hubClientService) : base(logger, dbContextFactory, dataRepositoryFactory)
        {
            _hubClientService = hubClientService;
        }
        public string ServiceSource { get; set; }
        public void StartPolling(string serviceSource)
        {
            ServiceSource = serviceSource;
            StartPolling();
        }
        public void StartPolling(Domain.Service service)
        {
            ServiceSource = service.Name;
            StartPolling();
        }
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
                        var blockingData = repo.Get().ToList();
                        if (blockingData.Count > 0)
                        {
                            // build polling monitor results
                            // send to the result to the hub for anyone listtening
                            _hubClientService.SendResult(ResultType.Blocking, BuildMonitorResult(blockingData));
                        }
                    }
                    catch (Exception ex)
                    {
                        _hubClientService.SendResult(ResultType.Blocking, new PollingMonitorResult(ex));
                    }
                    
                    Thread.Sleep(PollingInterval);
                    if (token.IsCancellationRequested)
                        break;
                }

            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private PollingMonitorResult BuildMonitorResult(IEnumerable<BlockingData> blockingData)
        {
            var monRes = new BlockingResult();
            blockingData.ToList().ForEach(bd =>
            {
                monRes.Results.Add(new Blocking(bd.LockType, bd.Database, bd.BlockObject,
                    bd.LockRequest, bd.WaiterSid, bd.WaitTime, bd.WaiterBatch,
                    bd.WaiterStatement, bd.BlockerSid, bd.BlockerBatch));
            });
            return new PollingMonitorResult(monRes);
        }

        ~BlockingPollingService()
        {
            StopPolling();
        }
    }
}
