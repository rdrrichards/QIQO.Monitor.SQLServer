using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QIQO.Monitor.Service
{
    public class BlockingWorker : BackgroundService
    {
        private readonly ILogger<BlockingWorker> _logger;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly IConfiguration _configuration;
        private readonly IDictionary<string, BlockingMonitorService> _servicesBeingMonitored = new Dictionary<string, BlockingMonitorService>();

        public BlockingWorker(ILogger<BlockingWorker> logger, IHttpClientHelper httpClientHelper, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientHelper = httpClientHelper;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(int.Parse(_configuration["Settings:WaitInterval"]), stoppingToken);

            try
            {
                var services = await _httpClientHelper.Get<IEnumerable<Service>>("Services");
                var servicesToMonitor = services.Where(s => s.Monitors.Any(m => m.MonitorType == MonitorType.SqlServer &&
                                                        m.MonitorCategory == MonitorCategories.DetectBlocking));
                _logger.LogInformation(servicesToMonitor.Count().ToString());
                servicesToMonitor.ToList().ForEach(async service =>
                {
                    var blockingService = new BlockingMonitorService(Map(service), _logger, _httpClientHelper);
                    _servicesBeingMonitored.Add(service.ServiceName, blockingService);
                    await blockingService.StartPolling(int.Parse(_configuration["Settings:ServiceInterval"]));

                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex}");
            }
        }
        private Service Map(Service service)
        {
            var srv = service;
            srv.Monitors = service.Monitors.Where(m => m.MonitorCategory == MonitorCategories.DetectBlocking).ToList();
            return srv;
        }
    }
}
