using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private const string CACHE_KEY = "news_list";

        public NewsController(IMemoryCache cache)
        {
            _cache = cache;
        }

        // ✅ GET /api/news
        [HttpGet]
        public IActionResult GetNews()
        {
            // Kiểm tra cache
            if (!_cache.TryGetValue(CACHE_KEY, out List<string> newsList))
            {
                // Giả lập danh sách tin tức
                newsList = new List<string>
                {
                    "Bản tin 1: ASP.NET Core ra mắt bản mới",
                    "Bản tin 2: Microsoft công bố .NET 9",
                    "Bản tin 3: C# được yêu thích nhất năm 2025"
                };

                // Cấu hình cache 60 giây
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
                };

                // Lưu cache
                _cache.Set(CACHE_KEY, newsList, cacheOptions);
            }

            return Ok(newsList);
        }

        // ✅ POST /api/news/clear-cache
        [HttpPost("clear-cache")]
        public IActionResult ClearCache()
        {
            _cache.Remove(CACHE_KEY);
            return Ok("Cache đã được xóa!");
        }
    }
}
