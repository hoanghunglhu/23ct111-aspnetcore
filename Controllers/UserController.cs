using LearnApiNetCore.Models;
  public class UserController : ControllerBase
  {
    [HttpGet]
    public string Get()
    {

      xxxxxxxxxxxxx
      var users = new List<Models.UserModel>();

      users.Add(new Models.UserModel { Id = 1, Name = "Nguyen Van A", Email = "" });
      users.Add(new Models.UserModel { Id = 2, Name = "Nguyen Van B", Email = "" });
      users.Add(new Models.UserModel { Id = 3, Name = "Nguyen Van C", Email = "" });

      return JsonSerializer.Serialize(users);
    }

    [HttpGet("{id}")]
    public UserModel GetById(int id)
    {
      return new UserModel { Id = 1, Name = "Nguyen Van A", Email = "" };
    }
  }
}