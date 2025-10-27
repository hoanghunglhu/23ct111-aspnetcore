using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

public static class CacheKeys
{
    // Key dùng để cache danh sách tin tức
    public const string NewsList = "NewsListCacheKey"; 
}

[ApiController]
[Route("[controller]")] // Base route: /news
public class NewsController : ControllerBase
{
    private readonly ICacheService _cacheService;

    public NewsController(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    // Task 2 & 3: GET /news
    [HttpGet]
    public IActionResult Get()
    {
        var newsList = _cacheService.GetOrCreate(
            CacheKeys.NewsList,
            () => FetchNewsFromDatabase(), // Hàm lấy dữ liệu nếu cache miss
            durationSeconds: 60 // Cache 60 giây
        );

        return Ok(newsList);
    }

    // Task 4: POST /news/clear-cache
    [HttpPost("clear-cache")]
    public IActionResult ClearCache()
    {
        _cacheService.Remove(CacheKeys.NewsList);
        
        return Ok("News cache has been successfully cleared.");
    }

    // Hàm giả lập lấy dữ liệu từ DB
    private List<object> FetchNewsFromDatabase()
    {
        // Giả lập độ trễ
        Task.Delay(100).Wait(); 
        
        return Enumerable.Range(1, 5).Select(index => new
        {
            Id = index,
            Title = $"Tin Tức Số {index}",
            // Dùng timestamp để kiểm tra cache
            PublishedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), 
            Source = "Simulated Database"
        })
        .ToList<object>();
    }
}