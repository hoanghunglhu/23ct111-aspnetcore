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
        private static int _nextId = 4; // để tạo ID tự tăng

        public NewsController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public IActionResult GetNews()
        {
            if (!_cache.TryGetValue("news", out List<NewsItem> news))
            {
                news = new List<NewsItem>
                {
                    new NewsItem { Id = 1, Title = "Tin 1: Trường mở lớp mới" },
                    new NewsItem { Id = 2, Title = "Tin 2: Sinh viên đạt giải Olympic" },
                    new NewsItem { Id = 3, Title = "Tin 3: Lịch thi cuối kỳ đã có" }
                };

                _cache.Set("news", news, TimeSpan.FromMinutes(5));
            }

            return Ok(news);
        }

        [HttpPost]
        public IActionResult AddNews([FromBody] NewsItem newItem)
        {
            if (!_cache.TryGetValue("news", out List<NewsItem> news))
            {
                news = new List<NewsItem>();
            }

            newItem.Id = _nextId++;
            news.Add(newItem);

            _cache.Set("news", news, TimeSpan.FromMinutes(5));

            return Ok(newItem);
        }

        [HttpPost("clear-cache")]
        public IActionResult ClearCache()
        {
            _cache.Remove("news");
            return Ok(new { message = "Cache cleared successfully" });
        }
    }
}
