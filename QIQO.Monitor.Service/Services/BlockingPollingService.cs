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
        void StartPolling(Server server, Service service);
    }
    public class BlockingPollingService : PollingServiceBase, IBlockingPollingService
    {
        private readonly IHubClientService _hubClientService;

        public BlockingPollingService(ILogger<BlockingPollingService> logger, IDbContextFactory dbContextFactory,
            IDataRepositoryFactory dataRepositoryFactory, IHubClientService hubClientService) : base(logger, dbContextFactory, dataRepositoryFactory)
        {
            _hubClientService = hubClientService;
        }
        private Service Service { get; set; }
        private Server Server { get; set; }
        private Query Query { get; set; }
        public void StartPolling(Server server, Service service)
        {
            Server = server;
            Service = service;
            Query = Service.Monitors.FirstOrDefault(m => m.MonitorType == MonitorType.SqlServer)
                .Queries.FirstOrDefault(q => q.QueryCategory == QueryCategory.DetectBlocking);
            if (Query != null) StartPolling();
        }
        public override void StartPolling()
        {
            _logger.LogInformation("Blocking Poller started");
            var token = cancellationTokenSource.Token;
            var listener = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    _logger.LogInformation("Blocking Polling");
                    CreateContext(Service.ServiceSource);
                    var repo = _dataRepositoryFactory.GetDataRepository<IBlockingRepository>();
                    try
                    {
                        var blockingData = repo.Get(Query.QueryText).ToList();
                        if (blockingData.Count > 0)
                        {
                            // build polling monitor results
                            // send to the result to the hub for anyone listtening
                            _hubClientService.SendResult(ResultType.Blocking, BuildMonitorResult(blockingData));
                        }
                    }
                    catch (Exception ex)
                    {
                        _hubClientService.SendResult(ResultType.Blocking, new PollingMonitorResult(Server, Service, ex));
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
            return new PollingMonitorResult(Server, Service, monRes);
        }

        ~BlockingPollingService()
        {
            StopPolling();
            _logger.LogInformation("Blocking Poller stopped");
        }
    }
}
