using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LearnAspNetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<NewsController> _logger;
        private const string CacheKey = "news_list";

        public NewsController(IMemoryCache cache, ILogger<NewsController> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetNews()
        {
            if (!_cache.TryGetValue(CacheKey, out List<string>? news))
            {
                // Dữ liệu mẫu
                news = new List<string>
                {
                    "Tin 1: .NET 8 chính thức phát hành!",
                    "Tin 2: Visual Studio Code ra mắt bản cập nhật mới",
                    "Tin 3: AI đang thay đổi cách lập trình"
                };

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

                _cache.Set(CacheKey, news, cacheOptions);
                _logger.LogInformation("Cache mới được tạo cho danh sách tin tức.");
            }
            else
            {
                _logger.LogInformation("Lấy danh sách tin tức từ cache.");
            }

            return Ok(news);
        }

        [HttpPost("clear-cache")]
        public IActionResult ClearCache()
        {
            _cache.Remove(CacheKey);
            _logger.LogInformation("Cache đã bị xóa.");
            return Ok(new { message = "Cache đã được xóa." });
        }
    }
}