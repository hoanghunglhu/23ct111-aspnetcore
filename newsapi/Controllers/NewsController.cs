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
        private const string CacheKey = "news_list";

        // Dữ liệu giả trong bộ nhớ
        private static List<NewsItem> _newsList = new List<NewsItem>
        {
            new NewsItem { Id = 1, Title = "Tin số 1", Content = "tin tức 1" },
            new NewsItem { Id = 2, Title = "Tin số 2", Content = " tin tức 2" },
        };

        public NewsController(IMemoryCache cache)
        {
            _cache = cache;
        }

        // GET /news -> Trả về danh sách tin tức (có cache 60 giây)
        [HttpGet]
        public IActionResult GetAll()
        {
            if (!_cache.TryGetValue(CacheKey, out List<NewsItem>? cachedNews))
            {
                cachedNews = _newsList;

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
                };

                _cache.Set(CacheKey, cachedNews, cacheOptions);
                return Ok(new { source = "database", data = cachedNews });
            }

            return Ok(new { source = "cache", data = cachedNews });
        }

        //  POST /news -> Tạo mới tin tức
        [HttpPost]
        public IActionResult Create([FromBody] NewsItem newNews)
        {
            newNews.Id = _newsList.Max(n => n.Id) + 1;
            newNews.CreatedAt = DateTime.Now;

            _newsList.Add(newNews);

            // Xóa cache để dữ liệu mới được load lại
            _cache.Remove(CacheKey);

            return Ok(new { message = "Đã thêm tin tức mới!", data = newNews });
        }

        // POST /news/clear-cache -> Xóa cache thủ công
        [HttpPost("clear-cache")]
        public IActionResult ClearCache()
        {
            _cache.Remove(CacheKey);
            return Ok(new { message = "Đã xóa cache thành công!" });
        }
    }
}