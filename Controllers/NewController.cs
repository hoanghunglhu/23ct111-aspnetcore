using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

[ApiController]
[Route("")]
public class NewController : ControllerBase
{
    private readonly IMemoryCache _cache;
    private const string CacheKey = "NewsCacheKey";

    public NewController(IMemoryCache cache)
    {
        _cache = cache;
    }

    [HttpGet("news")]
    public IActionResult GetNews()
    {
        if (!_cache.TryGetValue(CacheKey, out List<string> newsList))
        {
            newsList = GetNewsFromDataSource();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            _cache.Set(CacheKey, newsList, cacheEntryOptions);
        }
        

        return Ok(newsList);
    }

    [HttpPost("news/clear-cache")]
    public IActionResult ClearCache()
    {
        _cache.Remove(CacheKey);
        return Ok(new { message = "Cache đã được xóa." });
    }

    private List<string> GetNewsFromDataSource()
    {
        return new List<string>
        {
            "Tin tức 1",
            "Tin tức 2",
            "Tin tức 3"
        };
    }
}
