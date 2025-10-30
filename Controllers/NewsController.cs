using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace NewsApi.Controllers
{
    [ApiController]
    [Route("news")]
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
                    "Tin 1: Hôm nay trời đẹp",
                    "Tin 2: bt back end",
                    "Tin 3: bt window form"
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
            return Ok(new { message = "Cache đã được xoá!" });
        }
    }
}
