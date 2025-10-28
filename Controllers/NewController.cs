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
    [Route("api/[controller]")]

    public class Cache : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;

        public Cache(AppDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet("news")]
        public async Task<IActionResult> GetUsersWithCache()
        {
            var cacheKey = "usersList";
            if (!_cache.TryGetValue(cacheKey, out List<User> users))
            {
                users = await _context.Users.ToListAsync();

                if (users == null || !users.Any())
                {
                    return NotFound();
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));

                _cache.Set(cacheKey, users, cacheEntryOptions);
            }
            return Ok(users);
        }

        [HttpPost("news/clear-cache")]
        public async Task<IActionResult> ClearCache()
        {
            var cacheKey = "usersList";
            if (!_cache.TryGetValue(cacheKey, out List<User> users))
            {
                return NotFound("Cache not found.");
            }
            _cache.Remove(cacheKey);
            return Ok();
        }

        [HttpPost("upload")]
        [RequestSizeLimit(52428800)] // 50 MB
        public async Task<IActionResult> UpLoadFile(IFormFile? file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".docx", ".zip", ".txt", ".rar" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("File type not allowed.");
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            Directory.CreateDirectory(path);

            var filePath = Path.Combine(path, file.FileName);
            if (System.IO.File.Exists(filePath))
            {
                return Conflict("A file with the same name already exists.");
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { file.FileName, file.Length });
        }
    }
}