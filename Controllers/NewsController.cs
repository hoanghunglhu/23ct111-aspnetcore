using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private const string CacheKey = "news_list";

        public NewsController(IMemoryCache cache)
        {
            _cache = cache;
        }

       
        [HttpGet]
        public IActionResult GetNews()
        {
            if (!_cache.TryGetValue(CacheKey, out List<string> newsList))
            {
                
                newsList = new List<string>
                {
                    "Tin tức 1: caching la gi",
                    "Tin tức 2: xoa cache",
                    "Tin tức 3: tra du lieu cache moi"
                };

              
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
                };

                _cache.Set(CacheKey, newsList, cacheOptions);
            }

            return Ok(newsList);
        }

       
        [HttpPost("clear-cache")]
        public IActionResult ClearCache()
        {
            _cache.Remove(CacheKey);
            return Ok("Cache đã được xóa!");
        }
    }
}
