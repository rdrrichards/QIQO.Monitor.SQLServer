using Microsoft.Extensions.Logging;
using QIQO.Monitor.Domain;
using QIQO.Monitor.SQLServer.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QIQO.Monitor.Service
{
    public class BlockingMonitorService : MonitorServiceBase<BlockingData>
    {
        private readonly Service _service;
        private readonly ILogger<BlockingWorker> _logger;
        private readonly IHttpClientHelper _httpClientHelper;

        public BlockingMonitorService(Service service, ILogger<BlockingWorker> logger, IHttpClientHelper httpClientHelper)
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
                payload = BuildMonitorResult(data as IEnumerable<BlockingData>);
                payload.healthStatus = healthStatus;
            }
            else
            {
                payload = new MonitorResultPayload(data as System.Exception);
                payload.healthStatus = HealthStatus.Unhealthy;
            }
            payload.serviceName = _service.ServiceName;
            payload.monitorName = _service.Monitors[0].MonitorName;
            await _httpClientHelper.Post("results", payload);
        }

        public override MonitorResultPayload BuildMonitorResult(IEnumerable<BlockingData> data)
        {
            if (data.Any())
            {
                var result = new Domain.BlockingResult
                {
                    results = data.Select(bd => new Domain.Blocking(bd.LockType, bd.Database, bd.BlockObject,
                        bd.LockRequest, bd.WaiterSid, bd.WaitTime, bd.WaiterBatch,
                        bd.WaiterStatement, bd.BlockerSid, bd.BlockerBatch))
                };
                return new MonitorResultPayload(result);
            }
            else
                return new MonitorResultPayload(new Domain.BlockingResult());
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
                        var repo = new BlockingRepository(context, new BlockingMap());
                        try
                        {
                            var data = repo.Get(_service.Monitors[0].Queries[0].QueryText);
                            if (data.Any())
                            {
                                _logger.LogInformation($"Blocking on {_service.ServiceName}");
                                await AssessUnhealthy(data);
                            }
                            else
                            {
                                _logger.LogInformation($"No Blocking on {_service.ServiceName}");
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
