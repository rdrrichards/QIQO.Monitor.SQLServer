using Microsoft.Extensions.Logging;
using QIQO.Monitor.Domain;
using QIQO.Monitor.SQLServer.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QIQO.Monitor.Service
{
    public class OpenTransactionMonitorService : MonitorServiceBase<OpenTransactionData>
    {
        private readonly Service _service;
        private readonly ILogger<OpenTransactionWorker> _logger;
        private readonly IHttpClientHelper _httpClientHelper;

        public OpenTransactionMonitorService(Service service, ILogger<OpenTransactionWorker> logger, IHttpClientHelper httpClientHelper)
        {
            _service = service;
            _logger = logger;
            _httpClientHelper = httpClientHelper;
        }
        public override async Task Assess(HealthStatus healthStatus, object data)
        {
            MonitorResultPayload payload;
            if (data is not System.Exception)
            {
                payload = BuildMonitorResult(data as IEnumerable<OpenTransactionData>);
                payload.healthStatus = healthStatus;
            }
            else
            {
                payload = new MonitorResultPayload(data as System.Exception);
                payload.healthStatus = HealthStatus.Unhealthy;
            }
            payload.serviceName = _service.ServiceName;
            payload.monitorName = _service.Monitors.First().MonitorName;
            await _httpClientHelper.Post("results", payload);
        }

        public override MonitorResultPayload BuildMonitorResult(IEnumerable<OpenTransactionData> data)
        {
            if (data.Any())
            {
                var result = new Domain.OpenTransactionResult
                {
                    results = data.Select(otd => new Domain.OpenTransaction(otd.SessionId, otd.HostName, otd.LoginName,
                        otd.TransactionID, otd.TransactionName, otd.TransactionBegan, otd.DatabaseId, otd.DatabaseName))
                };
                return new MonitorResultPayload(result);
            }
            else
                return new MonitorResultPayload(new Domain.OpenTransactionResult());
        }

        public async override Task StartPolling(int inverval = 10000)
        {
            await Task.Run(async () =>
            {
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    _logger.LogInformation($"Starting monitoring for blocking for service {_service.ServiceName}");
                    using (var context = new SqlServerDbContext(CreateConnectionString(_service.ServiceSource)))
                    {
                        var repo = new OpenTransactionRepository(context, new OpenTransactionMap());
                        try
                        {
                            var data = repo.Get(_service.Monitors.First().Queries.First().QueryText);
                            if (data.Any())
                            {
                                _logger.LogInformation($"OpenTransaction on {_service.ServiceName}");
                                await AssessUnhealthy(data);
                            }
                            else
                            {
                                _logger.LogInformation($"No OpenTransaction on {_service.ServiceName}");
                                await AssessHealthy(data);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            _logger.LogError(ex.Message);
                            await AssessUnhealthy(ex);
                        }
                    }
                    await Task.Delay(inverval, cancellationTokenSource.Token);
                }
            });
        }
    }
}
