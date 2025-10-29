using LearnApiNetCore.Entity;
using LearnApiNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace LearnApiNetCore.Controllers
{
  [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        // ✅ Tạo list users trong controller
        private static List<UserModel> users = new List<UserModel>
        {
            new UserModel { Id = 1, Name = "Tuấn", Email = "tuan@example.com", Phone = "0123456789", Address = "HCM" },
            new UserModel { Id = 2, Name = "An", Email = "an@example.com", Phone = "0987654321", Address = "HN" },
            new UserModel { Id = 3, Name = "Hùng", Email = "hung@example.com", Phone = "0909090909", Address = "ĐN" }
        };

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(users);
        }
    }
}