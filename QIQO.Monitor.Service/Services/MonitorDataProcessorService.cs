using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QIQO.MQ;

namespace QIQO.Monitor.Service
{
    public interface IMonitorDataProcessorService {
        void StartProcessing();
    }
    public class MonitorDataProcessorService : IMonitorDataProcessorService
    {
        private readonly ILogger<MonitorDataProcessorService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMQConsumer _qConsumer;

        public MonitorDataProcessorService(ILogger<MonitorDataProcessorService> logger, IConfiguration configuration, IMQConsumer qConsumer)
        {
            _logger = logger;
            _configuration = configuration;
            _qConsumer = qConsumer;
            StartProcessing();
        }
        public void StartProcessing()
        {
            _logger.LogInformation("Monitor Data Processor Service started");
            _qConsumer.Pull(_configuration["QueueConfig:Monitor:Exchange"],
                                _configuration["QueueConfig:Monitor:AddQueue"], "#", ProcessMonitorData);
        }
        private void ProcessMonitorData(string monitorDataType, string dataJSON)
        {
            _logger.LogDebug($"{monitorDataType}: {dataJSON}");
        }
    }
}
