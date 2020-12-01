using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Data
{
    public class CoreCacheService : ICoreCacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IServerRepository _serverRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IQueryRepository _queryRepository;
        private readonly IMonitorRepository _monitorRepository;
        private readonly IMonitorQueryRepository _monitorQueryRepository;
        private readonly IEnvironmentRepository _environmentRepository;
        private readonly IEnvironmentServerRepository _environmentServerRepository;
        private readonly IEnvironmentServiceRepository _environmentServiceRepository;
        private readonly IServiceMonitorRepository _serviceMonitorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAttributeTypeRepository _attributeTypeRepository;
        private readonly IAttributeDataTypeRepository _attributeDataTypeRepository;
        private readonly IServiceMonitorAttributeRepository _serviceMonitorAttributeRepository;
        private readonly ILogger<CoreCacheService> _logger;
        private readonly MemoryCacheEntryOptions _cacheEntryOptions = new MemoryCacheEntryOptions() { SlidingExpiration = new TimeSpan(0, 20, 0) };

        public CoreCacheService(IMemoryCache memoryCache, IServerRepository serverRepository, IServiceRepository serviceRepository,
            IQueryRepository queryRepository, IMonitorRepository monitorRepository, IMonitorQueryRepository monitorQueryRepository,
            IEnvironmentRepository environmentRepository, IEnvironmentServerRepository environmentServerRepository,
            IEnvironmentServiceRepository environmentServiceRepository, IServiceMonitorRepository serviceMonitorRepository,
            ICategoryRepository categoryRepository, IAttributeTypeRepository attributeTypeRepository,
            IAttributeDataTypeRepository attributeDataTypeRepository, IServiceMonitorAttributeRepository serviceMonitorAttributeRepository,
            ILogger<CoreCacheService> logger)
        {
            _memoryCache = memoryCache;
            _serverRepository = serverRepository;
            _serviceRepository = serviceRepository;
            _queryRepository = queryRepository;
            _monitorRepository = monitorRepository;
            _monitorQueryRepository = monitorQueryRepository;
            _environmentRepository = environmentRepository;
            _environmentServerRepository = environmentServerRepository;
            _environmentServiceRepository = environmentServiceRepository;
            _serviceMonitorRepository = serviceMonitorRepository;
            _categoryRepository = categoryRepository;
            _attributeTypeRepository = attributeTypeRepository;
            _attributeDataTypeRepository = attributeDataTypeRepository;
            _serviceMonitorAttributeRepository = serviceMonitorAttributeRepository;
            _logger = logger;
            InitializeCache();
        }
        private void InitializeCache()
        {
            _logger.LogInformation($"Initializing {nameof(CoreCacheService)}");
            InitializeCache(() => _serverRepository.GetAll(), true);
            InitializeCache(() => _queryRepository.GetAll(), true);
            InitializeCache(() => _serviceRepository.GetAll(), true);
            InitializeCache(() => _monitorRepository.GetAll(), true);
            InitializeCache(() => _environmentRepository.GetAll(), true);
            InitializeCache(() => _monitorQueryRepository.GetAll(), true);
            InitializeCache(() => _environmentServerRepository.GetAll(), true);
            InitializeCache(() => _environmentServiceRepository.GetAll(), true);
            InitializeCache(() => _serviceMonitorRepository.GetAll(), true);
            InitializeCache(() => _categoryRepository.GetAll(), true);
            InitializeCache(() => _attributeTypeRepository.GetAll(), true);
            InitializeCache(() => _attributeDataTypeRepository.GetAll(), true);
            InitializeCache(() => _serviceMonitorAttributeRepository.GetAll(), true);
        }
        public void RefreshCache()
        {
            _logger.LogInformation($"Refreshing {nameof(CoreCacheService)}");
            InitializeCache();
        }

        private void InitializeCache<T>(Func<IEnumerable<T>> getAll, bool force = false)
        {
            if (!(_memoryCache.Get(typeof(T).Name) is IReadOnlyList<T>) || force)
            {
                _memoryCache.Set(typeof(T).Name, getAll.Invoke().ToList().AsReadOnly(), _cacheEntryOptions);
            }
        }
        public IReadOnlyList<T> GetCacheItem<T>()
        {
            if (!(_memoryCache.Get(typeof(T).Name) is IReadOnlyList<T>))
                InitializeCache();

            return _memoryCache.Get<IReadOnlyList<T>>(typeof(T).Name);
        }
        public IEnumerable<ServerData> GetServers() => GetCacheItem<ServerData>();
        public ServerData GetServer(int serverKey) => GetServers().FirstOrDefault(s => s.ServerKey == serverKey);
        public IEnumerable<QueryData> GetQueries() => GetCacheItem<QueryData>();
        public IEnumerable<QueryData> GetQueries(int monitorKey) => GetQueries().Join(GetMonitorQueries().Where(s => s.MonitorKey == monitorKey), q => q.QueryKey, m => m.QueryKey, (q, m) => q);
        public QueryData GetQuery(string name) => GetQueries().FirstOrDefault(q => q.Name == name);
        public QueryData GetQuery(int id) => GetQueries().FirstOrDefault(q => q.QueryKey == id);


        public IEnumerable<ServiceData> GetServices() => GetCacheItem<ServiceData>();
        public ServiceData GetService(int serviceKey) => GetServices().FirstOrDefault(s => s.ServiceKey == serviceKey);
        public IEnumerable<ServiceData> GetServices(int serverKey) => GetServices().Where(s => s.ServerKey == serverKey);

        private MemoryCacheEntryOptions GetMemoryCacheEntryOptions() =>
            new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20));
            //.SetPriority(CacheItemPriority.NeverRemove);

        public IEnumerable<MonitorData> GetMonitors() => GetCacheItem<MonitorData>();
        public IEnumerable<MonitorData> GetMonitors(int serviceType) => GetMonitors().Where(s => s.MonitorTypeKey == serviceType);

        public IEnumerable<MonitorQueryData> GetMonitorQueries() => GetCacheItem<MonitorQueryData>();

        public IEnumerable<EnvironmentData> GetEnvironments() => GetCacheItem<EnvironmentData>();

        public IEnumerable<EnvironmentServerData> GetEnvironmentServers() => GetCacheItem<EnvironmentServerData>();

        public IEnumerable<EnvironmentServiceData> GetEnvironmentServices() => GetCacheItem<EnvironmentServiceData>();
        public IEnumerable<EnvironmentData> GetServerEnvironments(int serverKey) => GetEnvironments().Join(GetEnvironmentServers()
            .Where(s => s.ServerKey == serverKey), e => e.EnvironmentKey, x => x.EnvironmentKey, (e, x) => e);
        public IEnumerable<EnvironmentData> GetServiceEnvironments(int serviceKey) => GetEnvironments().Join(GetEnvironmentServices()
            .Where(s => s.ServiceKey == serviceKey), e => e.EnvironmentKey, x => x.EnvironmentKey, (e, x) => e);

        public IEnumerable<ServiceMonitorData> GetServiceMonitors() => GetCacheItem<ServiceMonitorData>();
        public ServiceMonitorData GetServiceMonitors(int serviceKey, int monitorKey) => GetServiceMonitors()
            .FirstOrDefault(sm => sm.ServiceKey == serviceKey && sm.MonitorKey == monitorKey);

        public IEnumerable<MonitorData> GetServiceMonitors(int serviceKey) => GetMonitors().Join(GetServiceMonitors()
            .Where(s => s.ServiceKey == serviceKey), m => m.MonitorKey, x => x.MonitorKey, (m, x) => m);
        public IEnumerable<MonitorData> GetActiveServiceMonitors(int serviceKey) => GetMonitors()
            .Join(GetServiceMonitorAttributes()
            .Where(s => s.ServiceKey == serviceKey && s.AttributeTypeKey == 1 && s.AttributeValue == "1"), m => m.MonitorKey, x => x.MonitorKey, (m, x) => m);
        public IEnumerable<CategoryData> GetCategories() => GetCacheItem<CategoryData>();
        public IEnumerable<AttributeTypeData> GetAttributeTypes() => GetCacheItem<AttributeTypeData>();
        public IEnumerable<AttributeDataTypeData> GetAttributeDataTypes() => GetCacheItem<AttributeDataTypeData>();
        public IEnumerable<ServiceMonitorAttributeData> GetServiceMonitorAttributes() => GetCacheItem<ServiceMonitorAttributeData>();
        public IEnumerable<ServiceMonitorAttributeData> GetServiceMonitorAttributes(int serviceKey, int monitorKey) =>
            GetServiceMonitorAttributes().Where(a => a.ServiceKey == serviceKey && a.MonitorKey == monitorKey);
    }
}
