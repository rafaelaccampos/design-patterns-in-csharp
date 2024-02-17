using Microsoft.Extensions.Caching.Memory;

namespace DesignPatterns.Structural.Proxy
{
    public class MemoryCacheWrapper : IMemoryCacheWrapper
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryCacheWrapper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public object GetOrCreate(string key, Func<ICacheEntry, object> cacheFactory)
        {
            return _memoryCache.GetOrCreate(key, cacheFactory)!;
        }
    }
}
