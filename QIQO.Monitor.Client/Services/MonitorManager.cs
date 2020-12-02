using Microsoft.Extensions.Logging;
using QIQO.Monitor.Data;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Client
{
    public interface IMonitorManager
    {
        List<MonitorModel> GetMonitors();
        List<MonitorModel> GetMonitors(int serviceKey);
        MonitorModel AddMonitor(MonitorAdd environment);
        MonitorModel UpdateMonitor(int environmentKey, MonitorUpdate environment);
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
        public List<MonitorModel> GetMonitors()
        {
            var monitorData = _cacheService.GetMonitors().ToList();
            var monitors = new List<MonitorModel>();
            monitorData.ForEach(m =>
            {
                var queries = _queryManager.GetQueries(m.MonitorKey);
                monitors.Add(new MonitorModel(m, queries));
            });
            return monitors;
        }
        public List<MonitorModel> GetMonitors(int serviceKey)
        {
            var monitorData = _cacheService.GetServiceMonitors(serviceKey).ToList();
            var monitors = new List<MonitorModel>();
            monitorData.ForEach(m =>
            {
                var queries = _queryManager.GetQueries(m.MonitorKey);
                monitors.Add(new MonitorModel(m, queries, GetMonitorProperties(serviceKey, m.MonitorKey)));
            });
            return monitors;
        }
        public MonitorModel AddMonitor(MonitorAdd monitor)
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
        public MonitorModel UpdateMonitor(int monitorKey, MonitorUpdate monitor)
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
        private List<MonitorProperty> GetMonitorProperties(int serviceKey, int monitorKey)
        {
            return _cacheService.GetServiceMonitorAttributes(serviceKey, monitorKey).ToList()
                .Join(_cacheService.GetAttributeTypes(), a => a.AttributeTypeKey, t => t.AttributeTypeKey, (a, t)
                    => new { PropertyType = t.AttributeTypeName, PropertyValue = a.AttributeValue, t.AttributeDataTypeKey })
                    .Join(_cacheService.GetAttributeDataTypes(), n => n.AttributeDataTypeKey, d => d.AttributeDataTypeKey, (n, d)
                    => new MonitorProperty(n.PropertyType, d.AttributeDataTypeName, n.PropertyValue)).ToList();
        }
    }
}
