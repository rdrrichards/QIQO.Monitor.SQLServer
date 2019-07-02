using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QIQO.Monitor.Domain;
using QIQO.Monitor.SQLServer.Data;
using QIQO.MQ;
using System.Collections.Generic;

namespace QIQO.Monitor.Service
{
    public interface IMonitorDataProcessorService
    {
        void StartProcessing();
    }
    public class MonitorDataProcessorService : IMonitorDataProcessorService
    {
        private readonly ILogger<MonitorDataProcessorService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMQConsumer _qConsumer;
        private readonly IDataRepositoryFactory _dataRepositoryFactory;

        public MonitorDataProcessorService(ILogger<MonitorDataProcessorService> logger, IConfiguration configuration,
            IMQConsumer qConsumer, IDataRepositoryFactory dataRepositoryFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _qConsumer = qConsumer;
            _dataRepositoryFactory = dataRepositoryFactory;
            StartProcessing();
        }
        public void StartProcessing()
        {
            _logger.LogInformation("Monitor Data Processor Service started");
            _qConsumer.Dequeue(_configuration["QueueConfig:Monitor:Exchange"],
                                _configuration["QueueConfig:Monitor:AddQueue"], "#", ProcessMonitorData);
        }
        private void ProcessMonitorDataA(string monitorDataType, string dataJSON)
        {
            _logger.LogDebug($"{monitorDataType}: {dataJSON}");
        }
        private bool ProcessMonitorData(string monitorDataType, string dataJSON)
        {
            try
            {
                // _logger.LogDebug($"{monitorDataType}: {dataJSON}");
                switch (monitorDataType)
                {
                    case MonitorConstants.WaitStats:
                        _dataRepositoryFactory.GetDataRepository<IWaitStatsLogRepository>()
                            .InsertAll(JsonConvert.DeserializeObject<IEnumerable<WaitStatsLogData>>(dataJSON));
                        break;
                    default:
                        return false;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error processing {monitorDataType} data in MonitorDataProcessorService", ex);
                return false;
            }
        }
    }
}
