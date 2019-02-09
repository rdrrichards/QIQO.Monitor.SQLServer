using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.SQLServer.Data
{
    public class CoreCacheService : ICoreCacheService
    {
        private readonly IMemoryCache _cache;
        private readonly IServerRepository _serverRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IQueryRepository _queryRepository;
        private readonly IMonitorRepository _monitorRepository;
        private readonly IMonitorQueryRepository _monitorQueryRepository;
        private readonly ILogger<CoreCacheService> _logger;

        public CoreCacheService(IMemoryCache memoryCache, IServerRepository serverRepository, IServiceRepository serviceRepository,
            IQueryRepository queryRepository, IMonitorRepository monitorRepository, IMonitorQueryRepository monitorQueryRepository,
            ILogger<CoreCacheService> logger)
        {
            _cache = memoryCache;
            _serverRepository = serverRepository;
            _serviceRepository = serviceRepository;
            _queryRepository = queryRepository;
            _monitorRepository = monitorRepository;
            _monitorQueryRepository = monitorQueryRepository;
            _logger = logger;
            _logger.LogInformation($"Initializing {nameof(CoreCacheService)}");
            GetServers();
            GetQueries();
            GetServices();
            GetMonitors();
            GetMonitorQueries();
        }
        public IEnumerable<ServerData> GetServers()
        {
            if (!_cache.TryGetValue(CoreCacheKeys.Servers, out IEnumerable<ServerData> servers))
                servers = _cache.Set(CoreCacheKeys.Servers, _serverRepository.GetAll(), GetMemoryCacheEntryOptions());

            return servers;
        }
        public ServerData GetServer(int serverKey) => GetServers().FirstOrDefault(s => s.ServerKey == serverKey);
        public IEnumerable<QueryData> GetQueries()
        {
            if (!_cache.TryGetValue(CoreCacheKeys.Queries, out IEnumerable<QueryData> queries))
                queries = _cache.Set(CoreCacheKeys.Queries, _queryRepository.GetAll(), GetMemoryCacheEntryOptions());

            return queries;
        }
        public IEnumerable<QueryData> GetQueries(int monitorKey) => GetQueries().Join(GetMonitorQueries().Where(s => s.MonitorKey == monitorKey), q => q.QueryKey, m => m.QueryKey, (q, m) => q);
        public QueryData GetQuery(string name, int level) => GetQueries().FirstOrDefault(q => q.Name == name && q.LevelKey == level);
        public QueryData GetQuery(int id) => GetQueries().FirstOrDefault(q => q.QueryKey == id);


        public IEnumerable<ServiceData> GetServices()
        {
            if (!_cache.TryGetValue(CoreCacheKeys.Services, out IEnumerable<ServiceData> services))
                services = _cache.Set(CoreCacheKeys.Services, _serviceRepository.GetAll(), GetMemoryCacheEntryOptions());

            return services;
        }
        public ServiceData GetService(int serviceKey) => GetServices().FirstOrDefault(s => s.ServiceKey == serviceKey);
        public IEnumerable<ServiceData> GetServices(int serverKey) => GetServices().Where(s => s.ServerKey == serverKey);

        private MemoryCacheEntryOptions GetMemoryCacheEntryOptions() =>
            new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20))
            .SetPriority(CacheItemPriority.NeverRemove);

        public IEnumerable<MonitorData> GetMonitors()
        {
            if (!_cache.TryGetValue(CoreCacheKeys.Monitors, out IEnumerable<MonitorData> monitors))
                monitors = _cache.Set(CoreCacheKeys.Monitors, _monitorRepository.GetAll(), GetMemoryCacheEntryOptions());

            return monitors;
        }
        public IEnumerable<MonitorData> GetMonitors(int serviceType) => GetMonitors().Where(s => s.MonitorTypeKey == serviceType);

        public IEnumerable<MonitorQueryData> GetMonitorQueries()
        {
            if (!_cache.TryGetValue(CoreCacheKeys.MonitorQueries, out IEnumerable<MonitorQueryData> monitorQueries))
                monitorQueries = _cache.Set(CoreCacheKeys.MonitorQueries, _monitorQueryRepository.GetAll(), GetMemoryCacheEntryOptions());

            return monitorQueries;
        }
    }
}
