using QIQO.Monitor.SQLServer.Data;
using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace QIQO.Monitor.Api.Services
{
    public interface IMonitorManager
    {
        List<Monitor> GetMonitors();
        Monitor AddMonitor(MonitorAdd environment);
        Monitor UpdateMonitor(int environmentKey, MonitorUpdate environment);
        void DeleteMonitor(int environmentKey);
        List<MonitorCategory> GetMonitorCategories();
    }
    public class MonitorManager : ManagerBase, IMonitorManager
    {
        private readonly ICoreCacheService _cacheService;
        private readonly IMonitorEntityService _monitorEntityService;
        private readonly IMonitorRepository _monitorRepository;
        private readonly IQueryManager _queryManager;

        public MonitorManager(ILogger<MonitorManager> logger, ICoreCacheService cacheService,
            IMonitorEntityService monitorEntityService, IMonitorRepository monitorRepository,
            IQueryManager queryManager) : base(logger)
        {
            _cacheService = cacheService;
            _monitorEntityService = monitorEntityService;
            _monitorRepository = monitorRepository;
            _queryManager = queryManager;
        }
        public List<Monitor> GetMonitors()
        {
            var monitorData = _cacheService.GetMonitors().ToList();
            var monitors = new List<Monitor>();
            monitorData.ForEach(m => {
                var queries = _queryManager.GetQueries(m.MonitorKey);
                monitors.Add(new Monitor(m, queries));
            });
            return monitors;
        }
        public Monitor AddMonitor(MonitorAdd monitor)
        {
            return ExecuteOperation(() =>
            {
                var endData = new MonitorData
                {
                    MonitorName = monitor.MonitorName,
                    CategoryKey = (int)monitor.MonitorCategory,
                    LevelKey = (int)monitor.MonitorLevel,
                    MonitorTypeKey = (int)monitor.MonitorType
                };
                _monitorRepository.Insert(endData);
                _cacheService.RefreshCache();
                return GetMonitors().FirstOrDefault(e => e.MonitorName == monitor.MonitorName);
            });
        }
        public Monitor UpdateMonitor(int monitorKey, MonitorUpdate monitor)
        {
            return ExecuteOperation(() =>
            {
                var endData = new MonitorData
                {
                    MonitorKey = monitorKey,
                    MonitorName = monitor.MonitorName,
                    CategoryKey = (int)monitor.MonitorCategory,
                    LevelKey = (int)monitor.MonitorLevel,
                    MonitorTypeKey = (int)monitor.MonitorType
                };
                _monitorRepository.Save(endData);
                _cacheService.RefreshCache();
                return GetMonitors().FirstOrDefault(e => e.MonitorKey == monitorKey);
            });
        }
        public void DeleteMonitor(int monitorKey)
        {
            ExecuteOperation(() =>
            {
                var endData = new MonitorData { MonitorKey = monitorKey };
                _monitorRepository.Delete(endData);
                _cacheService.RefreshCache();
            });
        }
        public List<MonitorCategory> GetMonitorCategories()
        {
            var catVMs = new List<MonitorCategory>();
            var cats = _cacheService.GetCategories().ToList();
            cats.ForEach(c =>
            {
                catVMs.Add(new MonitorCategory { CategoryKey = c.CategoryKey, CategoryName = c.CategoryName });
            });
            return catVMs;
        }
    }
}
