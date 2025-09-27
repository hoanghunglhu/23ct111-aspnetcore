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
      var users = _context.Users.ToList();
      return Ok(users);
    }

    [HttpPost]
    public IActionResult Create(UserModel model)
    {
      var user = new User
      {
        name = model.name,
        email = model.email,
        phone = model.phone,
        address = model.address,
        birthday = model.birthday,
        gender = model.gender
      };

      users.Add(new Models.UserModel { Id = 1, Name = "Nguyen Van A", Email = "" });
      users.Add(new Models.UserModel { Id = 2, Name = "Nguyen Van B", Email = "" });
      users.Add(new Models.UserModel { Id = 3, Name = "Nguyen Van C", Email = "" });

      return CreatedAtAction(nameof(GetById), new { id = user.id }, user);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      var user = _context.Users.Find(id);
      if (user == null)
      {
        return NotFound();
      }
      return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UserModel model)
    {
      var user = _context.Users.Find(id);
      if (user == null)
      {
        return NotFound();
      }

      user.name = model.name;
      user.email = model.email;
      user.phone = model.phone;
      user.address = model.address;
      user.birthday = model.birthday;
      user.gender = model.gender;
      _context.SaveChanges();
      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      return new UserModel { Id = 1, Name = "Nguyen Van A", Email = "" };
    }
  }
}