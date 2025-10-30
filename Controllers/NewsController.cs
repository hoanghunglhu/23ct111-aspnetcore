using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnAspNetCore.Controllers
{
  
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public string? ImageUrl { get; set; } 
        public DateTime CreatedAt { get; set; }
    }

    [ApiController]
    [Route("news")]
    public class NewsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        private const string CacheKey = "news_list";

        public NewsController(IMemoryCache cache)
        {
            _cache = cache;
        }

       
        [HttpGet]
        public IActionResult GetNews()
        {
            if (!_cache.TryGetValue(CacheKey, out List<News> newsList))
            {
             
                newsList = GenerateFakeNews();

            
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(60))
                    .SetPriority(CacheItemPriority.High);

                _cache.Set(CacheKey, newsList, cacheOptions);

                Console.WriteLine(" Cache MISS → tạo dữ liệu mới và lưu vào cache.");
            }
            else
            {
                Console.WriteLine(" Cache HIT → lấy dữ liệu từ cache.");
            }

            return Ok(newsList);
        }

     
        [HttpPost("clear-cache")]
        public IActionResult ClearCache()
        {
            if (_cache.TryGetValue(CacheKey, out _))
            {
                _cache.Remove(CacheKey);
                Console.WriteLine($" Cache '{CacheKey}' đã bị xóa thủ công.");
                return Ok(new { message = $"Cache '{CacheKey}' cleared successfully." });
            }
            else
            {
                return Ok(new { message = $"Cache '{CacheKey}' is empty, nothing to clear." });
            }
        }
       [HttpPost("upload")]
        public IActionResult UploadNews([FromForm] string title, [FromForm] string content, IFormFile? image)
        {
            string? imageUrl = null;

            // Nếu có file gửi kèm
            if (image != null && image.Length > 0)
            {
                string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
                string filePath = Path.Combine(_uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                imageUrl = $"/uploads/{fileName}";
                Console.WriteLine($"File uploaded: {imageUrl}");
            }

            // Tạo bài mới
            var newPost = new News
            {
                Id = new Random().Next(1000, 9999),
                Title = title,
                Content = content,
                ImageUrl = imageUrl,
                CreatedAt = DateTime.Now
            };

            // Cập nhật cache (nếu có)
            List<News> newsList;
            if (!_cache.TryGetValue(CacheKey, out newsList))
                newsList = new List<News>();

            newsList.Add(newPost);
            _cache.Set(CacheKey, newsList);

            return Ok(new
            {
                message = "Thêm bài thành công!",
                data = newPost
            });
        }
      
        private List<News> GenerateFakeNews()
        {
            var random = new Random();
            var list = new List<News>();

            for (int i = 1; i <= 5; i++)
            {
                list.Add(new News
                {
                    Id = i,
                    Title = $"Bản tin số {i}",
                    Content = $"Nội dung bản tin {i}, sinh ngẫu nhiên lúc {DateTime.Now:T}",
                    CreatedAt = DateTime.Now.AddMinutes(-random.Next(0, 120))
                });
            }

          
            return list.OrderByDescending(n => n.CreatedAt).ToList();
        }
    }
}
