using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using LearnApiNetCore.Models;
using System;
namespace NewsApiProject.Controllers
{
    [ApiController]
    [Route("news")]
    public class NewsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private const string CacheKey = "NewsList";

        public NewsController(IMemoryCache cache)
        {
            _cache = cache;
        }

        // GET /news
        [HttpGet]
        public IActionResult GetNews()
        {
            if (!_cache.TryGetValue(CacheKey, out List<News> newsList))
            {
                newsList = new List<News>
                {
                    new News { Id = 1, Title = "Tin tức 1", Content = "Nội dung tin 1" },
                    new News { Id = 2, Title = "Tin tức 2", Content = "Nội dung tin 2" }
                };

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
                };

                _cache.Set(CacheKey, newsList, cacheOptions);
            }

            return Ok(newsList);
        }

        // POST /news/clear-cache
        [HttpPost("clear-cache")]
        public IActionResult ClearCache()
        {
            _cache.Remove(CacheKey);
            return Ok("Đã xóa cache.");
        }

        private class News
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }
    }
}
