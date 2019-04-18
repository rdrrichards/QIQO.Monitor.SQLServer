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
        private readonly IEnvironmentRepository _environmentRepository;
        private readonly IEnvironmentServerRepository _environmentServerRepository;
        private readonly IEnvironmentServiceRepository _environmentServiceRepository;
        private readonly IServiceMonitorRepository _serviceMonitorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAttributeTypeRepository _attributeTypeRepository;
        private readonly IAttributeDataTypeRepository _attributeDataTypeRepository;
        private readonly IServiceMonitorAttributeRepository _serviceMonitorAttributeRepository;
        private readonly ILogger<CoreCacheService> _logger;

        public CoreCacheService(IMemoryCache memoryCache, IServerRepository serverRepository, IServiceRepository serviceRepository,
            IQueryRepository queryRepository, IMonitorRepository monitorRepository, IMonitorQueryRepository monitorQueryRepository,
            IEnvironmentRepository environmentRepository, IEnvironmentServerRepository environmentServerRepository,
            IEnvironmentServiceRepository environmentServiceRepository, IServiceMonitorRepository serviceMonitorRepository,
            ICategoryRepository categoryRepository, IAttributeTypeRepository attributeTypeRepository,
            IAttributeDataTypeRepository attributeDataTypeRepository, IServiceMonitorAttributeRepository serviceMonitorAttributeRepository,
            ILogger<CoreCacheService> logger)
        {
            _cache = memoryCache;
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
            GetEnviroments();
            GetEnvironmentServers();
            GetEnvironmentServices();
            GetServers();
            GetQueries();
            GetServices();
            GetMonitors();
            GetMonitorQueries();
            GetServiceMonitors();
            GetAttributeTypes();
            GetAttributeDataTypes();
            GetServiceMonitorAttributes();
            GetCategories();
        }
        public void RefreshCache()
        {
            _logger.LogInformation($"Refreshing {nameof(CoreCacheService)}");
            _cache.Set(CoreCacheKeys.Servers, _serverRepository.GetAll(), GetMemoryCacheEntryOptions());
            _cache.Set(CoreCacheKeys.Queries, _queryRepository.GetAll(), GetMemoryCacheEntryOptions());
            _cache.Set(CoreCacheKeys.Services, _serviceRepository.GetAll(), GetMemoryCacheEntryOptions());
            _cache.Set(CoreCacheKeys.Monitors, _monitorRepository.GetAll(), GetMemoryCacheEntryOptions());
            _cache.Set(CoreCacheKeys.Environments, _environmentRepository.GetAll(), GetMemoryCacheEntryOptions());
            _cache.Set(CoreCacheKeys.MonitorQueries, _monitorQueryRepository.GetAll(), GetMemoryCacheEntryOptions());
            _cache.Set(CoreCacheKeys.EnvironmentServers, _environmentServerRepository.GetAll(), GetMemoryCacheEntryOptions());
            _cache.Set(CoreCacheKeys.EnvironmentServices, _environmentServiceRepository.GetAll(), GetMemoryCacheEntryOptions());
            _cache.Set(CoreCacheKeys.ServiceMonitors, _serviceMonitorRepository.GetAll(), GetMemoryCacheEntryOptions());
            _cache.Set(CoreCacheKeys.MonitorCategories, _categoryRepository.GetAll(), GetMemoryCacheEntryOptions());
            _cache.Set(CoreCacheKeys.AttributeTypes, _attributeTypeRepository.GetAll(), GetMemoryCacheEntryOptions());
            _cache.Set(CoreCacheKeys.AttributeDataTypes, _attributeDataTypeRepository.GetAll(), GetMemoryCacheEntryOptions());
            _cache.Set(CoreCacheKeys.ServiceMonitorAttributes, _serviceMonitorAttributeRepository.GetAll(), GetMemoryCacheEntryOptions());
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
        public QueryData GetQuery(string name) => GetQueries().FirstOrDefault(q => q.Name == name);
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

        public IEnumerable<EnvironmentData> GetEnviroments()
        {
            if (!_cache.TryGetValue(CoreCacheKeys.Environments, out IEnumerable<EnvironmentData> environments))
                environments = _cache.Set(CoreCacheKeys.Environments, _environmentRepository.GetAll(), GetMemoryCacheEntryOptions());
            return environments;
        }

        public IEnumerable<EnvironmentServerData> GetEnvironmentServers()
        {
            if (!_cache.TryGetValue(CoreCacheKeys.EnvironmentServers, out IEnumerable<EnvironmentServerData> environments))
                environments = _cache.Set(CoreCacheKeys.EnvironmentServers, _environmentServerRepository.GetAll(), GetMemoryCacheEntryOptions());
            return environments;
        }

        public IEnumerable<EnvironmentServiceData> GetEnvironmentServices()
        {
            if (!_cache.TryGetValue(CoreCacheKeys.EnvironmentServices, out IEnumerable<EnvironmentServiceData> environments))
                environments = _cache.Set(CoreCacheKeys.EnvironmentServices, _environmentServiceRepository.GetAll(), GetMemoryCacheEntryOptions());
            return environments;
        }
        public IEnumerable<EnvironmentData> GetServerEnvironments(int serverKey) => GetEnviroments().Join(GetEnvironmentServers()
            .Where(s => s.ServerKey == serverKey), e => e.EnvironmentKey, x => x.EnvironmentKey, (e, x) => e);
        public IEnumerable<EnvironmentData> GetServiceEnvironments(int serviceKey) => GetEnviroments().Join(GetEnvironmentServices()
            .Where(s => s.ServiceKey == serviceKey), e => e.EnvironmentKey, x => x.EnvironmentKey, (e, x) => e);

        public IEnumerable<ServiceMonitorData> GetServiceMonitors()
        {
            if (!_cache.TryGetValue(CoreCacheKeys.ServiceMonitors, out IEnumerable<ServiceMonitorData> monitors))
                monitors = _cache.Set(CoreCacheKeys.ServiceMonitors, _serviceMonitorRepository.GetAll(), GetMemoryCacheEntryOptions());
            return monitors;
        }
        public ServiceMonitorData GetServiceMonitors(int serviceKey, int monitorKey) => GetServiceMonitors()
            .FirstOrDefault(sm => sm.ServiceKey == serviceKey && sm.MonitorKey == monitorKey);

        public IEnumerable<MonitorData> GetServiceMonitors(int serviceKey) => GetMonitors().Join(GetServiceMonitors()
            .Where(s => s.ServiceKey == serviceKey), m => m.MonitorKey, x => x.MonitorKey, (m, x) => m);
        public IEnumerable<MonitorData> GetActiveServiceMonitors(int serviceKey) => GetMonitors()
            .Join(GetServiceMonitorAttributes()
            .Where(s => s.ServiceKey == serviceKey && s.AttributeTypeKey == 1 && s.AttributeValue == "1"), m => m.MonitorKey, x => x.MonitorKey, (m, x) => m);

        public IEnumerable<CategoryData> GetCategories()
        {
            if (!_cache.TryGetValue(CoreCacheKeys.MonitorCategories, out IEnumerable<CategoryData> monitorCategories))
                monitorCategories = _cache.Set(CoreCacheKeys.MonitorCategories, _categoryRepository.GetAll(), GetMemoryCacheEntryOptions());
            return monitorCategories;
        }

        public IEnumerable<AttributeTypeData> GetAttributeTypes()
        {
            if (!_cache.TryGetValue(CoreCacheKeys.AttributeTypes, out IEnumerable<AttributeTypeData> attributeTypes))
                attributeTypes = _cache.Set(CoreCacheKeys.AttributeTypes, _attributeTypeRepository.GetAll(), GetMemoryCacheEntryOptions());
            return attributeTypes;
        }

        public IEnumerable<AttributeDataTypeData> GetAttributeDataTypes()
        {
            if (!_cache.TryGetValue(CoreCacheKeys.AttributeDataTypes, out IEnumerable<AttributeDataTypeData> attributeDataTypes))
                attributeDataTypes = _cache.Set(CoreCacheKeys.AttributeDataTypes, _attributeDataTypeRepository.GetAll(), GetMemoryCacheEntryOptions());
            return attributeDataTypes;
        }

        public IEnumerable<ServiceMonitorAttributeData> GetServiceMonitorAttributes()
        {
            if (!_cache.TryGetValue(CoreCacheKeys.ServiceMonitorAttributes, out IEnumerable<ServiceMonitorAttributeData> serviceMonitorAttributes))
                serviceMonitorAttributes = _cache.Set(CoreCacheKeys.ServiceMonitorAttributes, _serviceMonitorAttributeRepository.GetAll(), GetMemoryCacheEntryOptions());
            return serviceMonitorAttributes;
        }
        public IEnumerable<ServiceMonitorAttributeData> GetServiceMonitorAttributes(int serviceKey, int monitorKey) =>
            GetServiceMonitorAttributes().Where(a => a.ServiceKey == serviceKey && a.MonitorKey == monitorKey);
    }
}
