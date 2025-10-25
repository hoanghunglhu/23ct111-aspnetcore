using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LearnApiNetCore.Controllers; 

[ApiController]
[Route("news")]
public class NewsController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;
    private const string NewsCacheKey = "NewsListCacheKey";

   
    private static List<News> _newsDatabase = new List<News>
    {
        new News { Id = 1, Title = "Tin tức nóng hổi số 1", Content = "Nội dung chi tiết...", PublishedDate = DateTime.UtcNow },
        new News { Id = 2, Title = "Một sự kiện vừa diễn ra", Content = "Nội dung chi tiết...", PublishedDate = DateTime.UtcNow },
        new News { Id = 3, Title = "Bản tin công nghệ", Content = "Nội dung chi tiết...", PublishedDate = DateTime.UtcNow }
    };

    public NewsController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    [HttpGet]
    public IActionResult Get()
    {
        if (_memoryCache.TryGetValue(NewsCacheKey, out List<News>? newsList))
        {
            Console.WriteLine("Dữ liệu được lấy từ Cache.");
            return Ok(newsList);
        }

        Console.WriteLine("Cache không có, dữ liệu được lấy từ Database.");
        
        
        newsList = _newsDatabase;

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

        _memoryCache.Set(NewsCacheKey, newsList, cacheEntryOptions);

        return Ok(newsList);
    }

  
    [HttpPost]
    public IActionResult CreateNews([FromBody] News newNews)
    {
        
        newNews.Id = _newsDatabase.Max(n => n.Id) + 1;
        newNews.PublishedDate = DateTime.UtcNow;

     
        _newsDatabase.Add(newNews);

        
        _memoryCache.Remove(NewsCacheKey);
        Console.WriteLine("Đã thêm tin tức mới và xóa cache.");

       
        return Ok(newNews);
    }

    [HttpPost("clear-cache")]
    public IActionResult ClearCache()
    {
        _memoryCache.Remove(NewsCacheKey);
        Console.WriteLine("Cache đã được xóa thủ công.");
        return Ok("Đã xóa cache thành công.");
    }
}