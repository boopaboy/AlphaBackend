using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace Business.Handlers;

public interface ICacheHandler<T>
{
    T? GetFromCache(string cacheKey);

    //Ai genererad metod, hjäpte mig att lägga till
    IEnumerable<TResult>? GetFromCache<TResult>(string cacheKey, Func<T, IEnumerable<TResult>> transform);

    T SetCache(string cacheKey, T data, int minutesToCache = 10);
}

public class CacheHandler<T>(IMemoryCache cache) : ICacheHandler<T>
{
    private readonly IMemoryCache _cache = cache;

    public T? GetFromCache(string cacheKey)
    {
        if (_cache.TryGetValue(cacheKey, out T? cachedData))
            return cachedData;

        return default;
    }
    //Ai genererad metod, hjäpte mig attlägga till
    public IEnumerable<TResult>? GetFromCache<TResult>(string cacheKey, Func<T, IEnumerable<TResult>> transform)
    {
        if (_cache.TryGetValue(cacheKey, out T? cachedData) && cachedData != null)
            return transform(cachedData);
        return default;
    }

    public T SetCache(string cacheKey, T data, int minutesToCache = 10)
    {
        _cache.Remove(cacheKey);
        _cache.Set(cacheKey, data, TimeSpan.FromMinutes(minutesToCache));
        return data;
    }

    public void RemoveCache(string cacheKey)
    {
        _cache.Remove(cacheKey);
    }

}
