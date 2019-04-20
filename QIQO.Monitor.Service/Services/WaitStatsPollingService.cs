using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QIQO.Monitor.Domain;
using QIQO.Monitor.Service.Polling;
using QIQO.Monitor.SQLServer.Data;
using QIQO.MQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QIQO.Monitor.Service.Services
{
    public interface IWaitStatsPollingService : IPollingService { }
    public class WaitStatsPollingService : PollingServiceBase<WaitStatsData>, IWaitStatsPollingService
    {
        private readonly IHubClientService _hubClientService;
        private readonly IConfiguration _configuration;
        private readonly IMQPublisher _qPublisher;

        public WaitStatsPollingService(ILogger<WaitStatsPollingService> logger, IDbContextFactory dbContextFactory,
            IDataRepositoryFactory dataRepositoryFactory, IHubClientService hubClientService, IHealthService healthService,
            IConfiguration configuration, IMQPublisher qPublisher)
            : base(logger, dbContextFactory, dataRepositoryFactory, healthService)
        {
            _hubClientService = hubClientService;
            _configuration = configuration;
            PollingInterval = int.TryParse(_configuration["Polling:WaitStatsPollingInterval"], out int interval) ? interval : (1000 * 60 * 5);
            _qPublisher = qPublisher;
        }
        public override void StartPolling(Server server, Service service)
        {
            Server = server;
            Service = service;
            Monitor = Service.Monitors.FirstOrDefault(m => m.MonitorType == MonitorType.SqlServer &&
                m.MonitorCategory == MonitorCategory.WaitStats);
            Query = Monitor.Queries.FirstOrDefault();
            if (Query != null) StartPolling();
        }
        public override void StartPolling()
        {
            _logger.LogInformation("WaitStats Poller started");
            var token = cancellationTokenSource.Token;
            var listener = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    _logger.LogInformation($"WaitStats Polling: Server: {Server.ServerName}; Service: {Service.ServiceName}; Monitor: {Monitor.MonitorName};");
                    CreateContext(Service.ServiceSource);
                    var repo = _dataRepositoryFactory.GetDataRepository<IWaitStatsRepository>();
                    try
                    {
                        var waitStatsData = repo.Get(Query.QueryText).ToList();
                        if (waitStatsData.Count > 0)
                        {
                            // Save the data for later analysis
                            _qPublisher.Enqueue(PrepData(waitStatsData, Service), _configuration["QueueConfig:Monitor:Exchange"],
                                _configuration["QueueConfig:Monitor:AddQueue"], MonitorConstants.WaitStats);
                            // Wais stats isn't an alertable thing without analysis of the data over time
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                    }

                    Thread.Sleep(PollingInterval);
                    if (token.IsCancellationRequested)
                        break;
                }

            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public override PollingMonitorResult BuildMonitorResult(IEnumerable<WaitStatsData> blockingData)
        {
            var monRes = new WaitStatsResult();
            blockingData.ToList().ForEach(bd =>
            {
                monRes.Results.Add(new WaitStats(bd.BatchNo ,bd.WaitType, bd.WaitPercentage, bd.AvgWaitSec,
                    bd.AvgResSec, bd.AvgSigSec, bd.WaitSec, bd.ResourceSec,
                    bd.SignalSec, bd.WaitCount));
            });
            return new PollingMonitorResult(Server, Service, Monitor, monRes);
        }
        private IEnumerable<WaitStatsLogData> PrepData(List<WaitStatsData> waitStatsData, Service service)
        {
            var logs = new List<WaitStatsLogData>();
            waitStatsData.ForEach(d => {
                logs.Add(new WaitStatsLogData {
                    AvgResSec = d.AvgResSec,
                    AvgSigSec = d.AvgSigSec,
                    AvgWaitSec = d.AvgWaitSec,
                    BatchNo = d.BatchNo,
                    ResourceSec = d.ResourceSec,
                    ServiceKey = service.ServiceKey,
                    SignalSec = d.SignalSec,
                    WaitCount = d.WaitCount,
                    WaitPercentage = d.WaitPercentage,
                    WaitSec = d.WaitSec,
                    WaitType = d.WaitType,
                    // WaitTypeKey = d.WaitTypeKey
                });
            });
            return logs;
        }
        ~WaitStatsPollingService()
        {
            _logger.LogInformation("WaitStats Poller stopping");
            StopPolling();
        }
    }
}
