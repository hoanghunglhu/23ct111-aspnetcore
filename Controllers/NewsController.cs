using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using LearnApiNetCore.Models; 

namespace LearnApiNetCore.Controllers
{
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "NewsListCacheKey";

        public NewsController(IMemoryCache memoryCache)
        {
            if (memoryCache == null)
            {
                throw new ArgumentNullException(nameof(memoryCache), "IMemoryCache không được null.");
            }
            _memoryCache = memoryCache;
        }

        [HttpGet("/news")]
        public IActionResult GetNews()
        {
            try
            {
                if (_memoryCache.TryGetValue(CacheKey, out List<NewsArticle>? cachedNews))
                {
                    if (cachedNews != null)
                    {
                        Console.WriteLine("Cache hit. Lấy dữ liệu tin tức từ cache."); 
                        return Ok(cachedNews);
                    }
                    else
                        Console.WriteLine("Cache hit nhưng dữ liệu bị null, sẽ lấy lại.");
                }

                Console.WriteLine("Cache miss. Đang lấy dữ liệu tin tức từ data source..."); 
                var newsList = GetNewsFromDataSource();

                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

                _memoryCache.Set(CacheKey, newsList, cacheOptions);

                return Ok(newsList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LỖI: {ex.Message}"); 
                return StatusCode(500, new { message = "Đã có lỗi phía máy chủ. Vui lòng thử lại sau." }); // <-- Tiếng Việt
            }
        }

        [HttpPost("/news/clear-cache")]
        public IActionResult ClearCache()
        {
            try
            {
                _memoryCache.Remove(CacheKey);
                Console.WriteLine("Cache tin tức đã được xóa thành công."); 

                return Ok(new { message = "Cache cho /news đã được xóa thành công." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LỖI: {ex.Message}"); 
                return StatusCode(500, new { message = "Đã có lỗi khi xóa cache." }); 
            }
        }
        private List<NewsArticle> GetNewsFromDataSource()
        {

            Thread.Sleep(1500); // Giả lập chờ 1.5 giây

            return new List<NewsArticle>
            {
                new NewsArticle
                {
                    id = 1,
                    title = "Tin tức nóng hổi 1",
                    content = "Nội dung chi tiết cho tin 1...",
                    publishedDate = DateTime.Now.AddHours(-1)
                },
                new NewsArticle
                {
                    id = 2,
                    title = "Tin tức quan trọng 2",
                    content = "Nội dung chi tiết cho tin 2...",
                    publishedDate = DateTime.Now.AddHours(-2)
                }
            };
        }
    }
}