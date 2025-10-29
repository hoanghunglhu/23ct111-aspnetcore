using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NewsApi.Models;

namespace NewsApi.Controllers
{
    // Đánh dấu đây là một API Controller
    [ApiController]
    // Đặt route là /news (dựa theo tên controller)
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        // Khai báo đối tượng memory cache
        private readonly IMemoryCache _cache;
        // Khóa lưu trữ dữ liệu cache
        private const string cacheKey = "news_cache_key";
        // Constructor nhận IMemoryCache thông qua dependency injection
        public NewsController(IMemoryCache cache)
        {
            _cache = cache;
        }
        // Phương thức GET: trả về danh sách tin tức
        [HttpGet]
        public IActionResult Get()
        {
            // Thử lấy dữ liệu từ cache
            if (!_cache.TryGetValue(cacheKey, out List<News> newsList))
            {
                // Nếu cache chưa có dữ liệu, tạo danh sách tin giả lập
                newsList = new List<News>
                {
                    new News { Id = 1, Title = "Tin 1", Content = "Nội dung tin 1", Timestamp = DateTime.UtcNow.ToString("o") },
                    new News { Id = 2, Title = "Tin 2", Content = "Nội dung tin 2", Timestamp = DateTime.UtcNow.ToString("o") },
                };
                // Cấu hình cache hết hạn sau 60 giây
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
                // Lưu danh sách vào cache
                _cache.Set(cacheKey, newsList, cacheEntryOptions);
            }
            // Trả về dữ liệu tin tức (dù từ cache hay mới tạo)
            return Ok(newsList);
        }
        // Phương thức POST: xóa cache thủ công
        [HttpPost("clear-cache")]
        public IActionResult ClearCache()
        {
            _cache.Remove(cacheKey);
            return Ok("Đã xoá cache!");
        }
    }
}
