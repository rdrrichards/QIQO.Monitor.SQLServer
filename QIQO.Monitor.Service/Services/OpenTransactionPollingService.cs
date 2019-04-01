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
    public interface IOpenTransactionPollingService : IPollingService { }
    public class OpenTransactionPollingService : PollingServiceBase<OpenTransactionData>, IOpenTransactionPollingService
    {
        private readonly IHubClientService _hubClientService;

        public OpenTransactionPollingService(ILogger<OpenTransactionPollingService> logger, IDbContextFactory dbContextFactory,
            IDataRepositoryFactory dataRepositoryFactory, IHubClientService hubClientService, IHealthService healthService) 
            : base(logger, dbContextFactory, dataRepositoryFactory, healthService)
        {
            _hubClientService = hubClientService;
        }
        public override void StartPolling(Server server, Service service)
        {
            Server = server;
            Service = service;
            Monitor = Service.Monitors.FirstOrDefault(m => m.MonitorType == MonitorType.SqlServer &&
                m.MonitorCategory == MonitorCategory.OpenTranactions);
            Query = Monitor.Queries.FirstOrDefault();
            if (Query != null) StartPolling();
        }
        public override void StartPolling()
        {
            _logger.LogInformation("Open Transaction Poller started");
            var token = cancellationTokenSource.Token;
            var listener = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    _logger.LogInformation($"Open Transaction Polling: Server: {Server.ServerName}; Service: {Service.ServiceName}; Monitor: {Monitor.MonitorName};");
                    CreateContext(Service.ServiceSource);
                    var repo = _dataRepositoryFactory.GetDataRepository<IOpenTransactionRepository>();
                    try
                    {
                        var openTxData = repo.Get(Query.QueryText).ToList();
                        if (openTxData.Count > 0)
                        {
                            // build polling monitor results
                            // send to the result to the hub for anyone listening
                            _hubClientService.SendResult(ResultType.OpenTransaction, BuildMonitorResult(openTxData));
                            AssessUnhealthy();
                        }
                        else
                            AssessHealthy();
                    }
                    catch (Exception ex)
                    {
                        _hubClientService.SendResult(ResultType.OpenTransaction, new PollingMonitorResult(Server, Service, Monitor, ex));
                        AssessUnhealthy();
                    }

                    Thread.Sleep(PollingInterval);
                    if (token.IsCancellationRequested)
                        break;
                }

            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public override PollingMonitorResult BuildMonitorResult(IEnumerable<OpenTransactionData> openTxData)
        {
            var monRes = new OpenTransactionResult();
            openTxData.ToList().ForEach(tx =>
            {
                monRes.Results.Add(new OpenTransaction(tx.SessionId, tx.HostName, tx.LoginName, tx.TransactionID,
                    tx.TransactionName, tx.TransactionBegan, tx.DatabaseId, tx.DatabaseName));
            });
            return new PollingMonitorResult(Server, Service, Monitor, monRes);
        }

        ~OpenTransactionPollingService()
        {
            _logger.LogInformation("Open Transaction Poller stopping");
            StopPolling();
        }
    }
}
