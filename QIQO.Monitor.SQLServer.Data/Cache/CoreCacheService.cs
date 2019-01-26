using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.SQLServer.Data
{
    public class CoreCacheService : ICoreCacheService
    {
        private readonly IMemoryCache _cache;
        private readonly IServerRepository _serverRepository;
        private readonly IQueryRepository _queryRepository;

        public CoreCacheService(IMemoryCache memoryCache, IServerRepository serverRepository, IQueryRepository queryRepository)
        {
            _cache = memoryCache;
            _serverRepository = serverRepository;
            _queryRepository = queryRepository;
            GetServers();
            GetQueries();
        }
        public IEnumerable<ServerData> GetServers()
        {
            if(!_cache.TryGetValue(CoreCacheKeys.Servers, out IEnumerable<ServerData> servers))
                _cache.Set(CoreCacheKeys.Servers, _serverRepository.GetAll(), GetMemoryCacheEntryOptions());

            return servers;
        }
        public ServerData GetServer(int serverKey) => GetServers().FirstOrDefault(s => s.ServerKey == serverKey);
        public IEnumerable<QueryData> GetQueries()
        {
            if (!_cache.TryGetValue(CoreCacheKeys.Queries, out IEnumerable<QueryData> queries))
                _cache.Set(CoreCacheKeys.Queries, _queryRepository.GetAll(), GetMemoryCacheEntryOptions());

            return queries;
        }
        public QueryData GetQuery(string name, int level) => GetQueries().FirstOrDefault(q => q.Name == name && q.LevelKey == level);
        public QueryData GetQuery(int id) => GetQueries().FirstOrDefault(q => q.QueryKey == id);
        private MemoryCacheEntryOptions GetMemoryCacheEntryOptions() => new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20));
    }
}
