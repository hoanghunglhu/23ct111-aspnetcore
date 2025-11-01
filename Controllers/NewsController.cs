using LearnApiNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory; 
using LearnApiNetCore.Entity;             
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace LearnApiNetCore.Entity
{
    [ApiController]
    [Route("news")]
    public class NewsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly AppDbContext _context;
        private const string CacheKey = "NewsListCacheKey";

        public NewsController(IMemoryCache memoryCache, AppDbContext context)
        {
            _cache = memoryCache;
            _context = context;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            if (_cache.TryGetValue(CacheKey, out List<NewsArticle> newsList))
            {
                return Ok(new { Source = "Cache", Data = newsList });
            }

            newsList = await _context.NewsArticles.AsNoTracking().ToListAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60)); 

            _cache.Set(CacheKey, newsList, cacheEntryOptions);

            return Ok(new { Source = "Database (Mới)", Data = newsList });
        }

        [HttpPost("clear-cache")]
        public IActionResult ClearCache()
        {
            _cache.Remove(CacheKey);
            return Ok(new { Message = "Đã xóa cache thành công." });
        }
    }
}

