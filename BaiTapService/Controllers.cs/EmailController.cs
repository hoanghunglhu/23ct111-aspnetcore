// Controllers/EmailController.cs
using Microsoft.AspNetCore.Mvc;
using BaiTapService.Services; // ĐÚNG NAMESPACE CỦA BẠN

namespace BaiTapService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] EmailRequest request)
        {
            try
            {
                await _emailService.SendEmailAsync(
                    request.To,
                    request.Subject,
                    request.Body,
                    request.IsHtml
                );
                return Ok(new { message = "Gửi email thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }

    public class EmailRequest
    {
        public string To { get; set; } = "";
        public string Subject { get; set; } = "No Subject";
        public string Body { get; set; } = "No Body";
        public bool IsHtml { get; set; } = true;
    }
}