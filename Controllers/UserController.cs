using LearnApiNetCore.Entity;
using LearnApiNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace LearnApiNetCore.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize] // Yêu cầu authentication cho tất cả endpoints
  public class UserController : ControllerBase
  {
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
      var users = new List<Models.UserModel>();

      users.Add(new Models.UserModel { Id = 1, Name = "Nguyen Van A", Email = "" });
      users.Add(new Models.UserModel { Id = 2, Name = "Nguyen Van B", Email = "" });
      users.Add(new Models.UserModel { Id = 3, Name = "Nguyen Van C", Email = "" });

      return CreatedAtAction(nameof(GetById), new { id = user.id }, user);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      return new UserModel { Id = 1, Name = "Nguyen Van A", Email = "" };
    }
  }
}