using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LearnApiNetCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private const string CacheKey = "NewsList";

        public NewsController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public IActionResult GetNews()
        {
            if (_cache.TryGetValue(CacheKey, out List<object> cachedNews))
            {
                return Ok(new
                {
                    source = "cache",
                    data = cachedNews
                });
            }

            var newsList = new List<object>
            {
                new { Id = 1, Title = "Tin buổi sáng", Content = "Hôm nay trời đẹp, nhiều tin mới." },
                new { Id = 2, Title = "Tin buổi trưa", Content = "Giá xăng dầu giảm nhẹ." },
                new { Id = 3, Title = "Tin buổi tối", Content = "Kết quả bóng đá hôm nay rất bất ngờ!" }
            };

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

            _cache.Set(CacheKey, newsList, cacheOptions);

            return Ok(new
            {
                source = "database",
                data = newsList
            });
        }

        [HttpPost("clear-cache")]
        public IActionResult ClearCache()
        {
            _cache.Remove(CacheKey);
            return Ok(new { message = "Đã xóa cache thành công!" });
        }
    }
}
