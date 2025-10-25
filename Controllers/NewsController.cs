using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory; // Dùng cho IMemoryCache
using LearnApiNetCore.Entity;             // Dùng cho AppDbContext và NewsArticle
using Microsoft.EntityFrameworkCore;       // Dùng cho ToListAsync()
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace LearnApiNetCore.Controllers
{
    [ApiController]
    [Route("news")]
    public class NewsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly AppDbContext _context; // Dùng để truy vấn DB
        private const string CacheKey = "NewsListCacheKey";

        // Yêu cầu cả 2 dịch vụ qua constructor
        public NewsController(IMemoryCache memoryCache, AppDbContext context)
        {
            _cache = memoryCache;
            _context = context;
        }

        // YÊU CẦU 2 & 3: GET /news, trả về danh sách và cache 60 giây
        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            // Thử lấy từ cache trước
            if (_cache.TryGetValue(CacheKey, out List<NewsArticle> newsList))
            {
                // Cache hit: Lấy được từ cache, trả về ngay
                return Ok(new { Source = "Cache", Data = newsList });
            }

            // Cache miss: Không có trong cache, phải đi lấy dữ liệu từ DB
            // Dùng await và ToListAsync() để lấy dữ liệu từ SQL Server
            newsList = await _context.NewsArticles.AsNoTracking().ToListAsync();

            // Đặt các tùy chọn cho cache
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Giữ cache trong 60 giây (theo yêu cầu)
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60)); 

            // Lưu dữ liệu vào cache
            _cache.Set(CacheKey, newsList, cacheEntryOptions);

            // Trả về dữ liệu vừa lấy được
            return Ok(new { Source = "Database (Mới)", Data = newsList });
        }

        // YÊU CẦU 4: POST /news/clear-cache
        [HttpPost("clear-cache")]
        public IActionResult ClearCache()
        {
            _cache.Remove(CacheKey);
            return Ok(new { Message = "Đã xóa cache thành công." });
        }
    }
}
