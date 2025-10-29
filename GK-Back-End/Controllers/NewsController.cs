using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NewsApi.Models;

namespace NewsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private const string cacheKey = "news_cache_key";

        public NewsController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (!_cache.TryGetValue(cacheKey, out List<News> newsList))
            {
                // Simulate data source
                newsList = new List<News>
                {
                    new News { Id = 1, Title = "Tin 1", Content = "Nội dung tin 1", Timestamp = DateTime.UtcNow.ToString("o") },
                    new News { Id = 2, Title = "Tin 2", Content = "Nội dung tin 2", Timestamp = DateTime.UtcNow.ToString("o") },
                };
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
                _cache.Set(cacheKey, newsList, cacheEntryOptions);
            }
            return Ok(newsList);
        }

        [HttpPost("clear-cache")]
        public IActionResult ClearCache()
        {
            _cache.Remove(cacheKey);
            return Ok("Đã xoá cache!");
        }
    }
}
