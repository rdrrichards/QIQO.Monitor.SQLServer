using QIQO.Monitor.SQLServer.Data;
using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Api.Services
{
    public interface IMonitorManager
    {
        List<Monitor> GetMonitors();
        Monitor AddMonitor(MonitorAdd environment);
        Monitor UpdateMonitor(int environmentKey, MonitorUpdate environment);
        void DeleteMonitor(int environmentKey);
    }
    public class MonitorManager : IMonitorManager
    {
        private readonly ICoreCacheService _cacheService;
        private readonly IMonitorEntityService _monitorEntityService;
        private readonly IMonitorRepository _monitorRepository;

        public MonitorManager(ICoreCacheService cacheService, IMonitorEntityService monitorEntityService, IMonitorRepository monitorRepository)
        {
            _cacheService = cacheService;
            _monitorEntityService = monitorEntityService;
            _monitorRepository = monitorRepository;
        }
        public List<Monitor> GetMonitors() => new List<Monitor>(_monitorEntityService.Map(_cacheService.GetMonitors().ToList()));
        public Monitor AddMonitor(MonitorAdd monitor)
        {
            var endData = new MonitorData { MonitorName = monitor.MonitorName,
                CategoryKey = (int)monitor.MonitorCategory,
                LevelKey = (int)monitor.MonitorLevel,
                MonitorTypeKey = (int)monitor.MonitorType
            };
            _monitorRepository.Insert(endData);
            _cacheService.RefreshCache();
            return GetMonitors().FirstOrDefault(e => e.MonitorName == monitor.MonitorName);
        }
        public Monitor UpdateMonitor(int monitorKey, MonitorUpdate monitor)
        {
            var endData = new MonitorData { MonitorKey = monitorKey, MonitorName = monitor.MonitorName,
                CategoryKey = (int)monitor.MonitorCategory, LevelKey = (int)monitor.MonitorLevel,
                MonitorTypeKey = (int)monitor.MonitorType };
            _monitorRepository.Save(endData);
            _cacheService.RefreshCache();
            return GetMonitors().FirstOrDefault(e => e.MonitorKey == monitorKey);
        }
        public void DeleteMonitor(int monitorKey)
        {
            var endData = new MonitorData { MonitorKey = monitorKey };
            _monitorRepository.Delete(endData);
            _cacheService.RefreshCache();
        }
    }
}
