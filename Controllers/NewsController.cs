using LearnApiNetCore.Entity;
using LearnApiNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace LearnApiNetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "news_cache";

        public NewsController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult GetNews()
        {
            // Nếu trong cache đã có dữ liệu thì lấy từ cache
            if (_memoryCache.TryGetValue(CacheKey, out List<NewsDTO>? news))
            {
                return Ok(new
                {
                    message = "Dữ liệu load từ cache",
                    data = news
                });
            }

            // Nếu chưa có thì tạo dữ liệu mẫu
            news = new List<NewsDTO>
            {
                new NewsDTO { Id = 1, Title = "Tuấn nè", PublishedAt = DateTime.Now, Source = "Microsoft" },
                new NewsDTO { Id = 2, Title = "Tuấn nữa nè", PublishedAt = DateTime.Now.AddMinutes(-10), Source = "FPT" }
            };

            // Lưu vào cache 60 giây
            _memoryCache.Set(CacheKey, news, TimeSpan.FromSeconds(60));

            return Ok(new
            {
                message = "Dữ liệu load từ database (cache now)",
                data = news
            });
        }

        // POST /news/clear-cache
        [HttpPost("clear-cache")]
        public IActionResult ClearCache()
        {
            _memoryCache.Remove(CacheKey);

            return Ok(new
            {
                message = "Đã xóa"
            });
        }
    }

    // Model DTO trả về JSON
    public class NewsDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime PublishedAt { get; set; }
        public string? Source { get; set; }
    }
}