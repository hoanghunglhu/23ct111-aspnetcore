using Microsoft.Extensions.Caching.Memory;
using System;

public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _cache = memoryCache;
    }

    public T GetOrCreate<T>(string key, Func<T> factory, int durationSeconds = 60)
    {
        if (_cache.TryGetValue(key, out T value))
        {
            Console.WriteLine($"[Cache Hit] Key: {key}");
            return value;
        }

        Console.WriteLine($"[Cache Miss] Key: {key}. Fetching data...");
        value = factory();

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(durationSeconds));

        _cache.Set(key, value, cacheEntryOptions);

        return value;
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
        Console.WriteLine($"[Cache Cleared] Key: {key} has been removed.");
    }
}