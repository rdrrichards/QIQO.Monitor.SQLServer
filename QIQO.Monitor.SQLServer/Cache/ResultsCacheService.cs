using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.SQLServer
{
    public class ResultsCacheService : IResultsCacheService
    {
        private readonly IMemoryCache _cache;

        public ResultsCacheService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        private MemoryCacheEntryOptions GetMemoryCacheEntryOptions() => new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20));
    }
}
