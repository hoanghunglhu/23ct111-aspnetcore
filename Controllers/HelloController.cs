using Microsoft.AspNetCore.Mvc;

namespace LearnApiNetCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase 
    {
        
        [HttpGet]
        public IActionResult SayHello()
        {
            string name = "World";
            return Ok($"Hello, {name}");
        }
    }
}