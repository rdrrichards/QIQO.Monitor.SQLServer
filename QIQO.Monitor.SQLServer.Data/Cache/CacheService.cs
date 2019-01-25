using Microsoft.Extensions.Caching.Memory;
using QIQO.Monitor.SQLServer.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.SQLServer.Data
{
    public interface ICacheService
    {
        IEnumerable<ServerData> GetServers();
        ServerData GetServer(int serverKey);
    }
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly IServerRepository _serverRepository;

        public CacheService(IMemoryCache memoryCache, IServerRepository serverRepository)
        {
            _cache = memoryCache;
            _serverRepository = serverRepository;
            GetServers();
        }
        public IEnumerable<ServerData> GetServers()
        {
            if(!_cache.TryGetValue(CacheKeys.Servers, out IEnumerable<ServerData> servers))
                _cache.Set(CacheKeys.Servers, _serverRepository.GetAll(), GetMemoryCacheEntryOptions());

            return servers;
        }
        public ServerData GetServer(int serverKey)
        {
            if (!_cache.TryGetValue(CacheKeys.Servers, out IEnumerable<ServerData> servers))
                _cache.Set(CacheKeys.Servers, _serverRepository.GetAll(), GetMemoryCacheEntryOptions());

            return servers.FirstOrDefault(s => s.ServerKey == serverKey);
        }
        private MemoryCacheEntryOptions GetMemoryCacheEntryOptions() => new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20));
    }
    public static class CacheKeys
    {
        public static string Servers { get { return "_Servers";  } }
    }
}
