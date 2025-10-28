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

        [HttpGet]
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

        [HttpPost]
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
    }
}