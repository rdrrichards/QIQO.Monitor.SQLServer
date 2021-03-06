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
        private readonly int _waitInterval;
        private readonly IDictionary<string, BlockingMonitorService> _servicesBeingMonitored = new Dictionary<string, BlockingMonitorService>();
        private readonly int _serviceInterval;

        public BlockingWorker(ILogger<BlockingWorker> logger, IHttpClientHelper httpClientHelper, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientHelper = httpClientHelper;
            _configuration = configuration;
            _waitInterval = int.Parse(_configuration["Settings:WaitInterval"]);
            _serviceInterval = int.Parse(_configuration["Settings:ServiceInterval"]);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(BlockingWorker)} running at: {DateTimeOffset.Now}");
            await Task.Delay(_waitInterval, stoppingToken);

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
                    await blockingService.StartPolling(_serviceInterval);

                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
        private static Service Map(Service service)
        {
            var srv = service;
            srv.Monitors = service.Monitors.Where(m => m.MonitorCategory == MonitorCategories.DetectBlocking).ToList();
            return srv;
        }
    }
}
