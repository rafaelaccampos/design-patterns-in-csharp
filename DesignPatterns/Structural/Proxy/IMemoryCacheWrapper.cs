using Microsoft.Extensions.Caching.Memory;

namespace DesignPatterns.Structural.Proxy
{
    public interface IMemoryCacheWrapper
    {
        object GetOrCreate(string key, Func<ICacheEntry, object> cacheFactory);
    }
}
