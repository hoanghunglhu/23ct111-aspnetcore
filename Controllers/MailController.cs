using LearnApiNetCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearnApiNetCore.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase 
    {
        private readonly IEmailService _emailService;

        public MailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-test")]
        public async Task<IActionResult> SendTestEmail([FromQuery] string recipientEmail)
        {
            try
            {
                string subject = "dotandu2018st@gmail.com";
                string body = "<h1>Happy Halloween!</h1><p>Email này được gửi từ ASP.NET</p>";

                await _emailService.SendEmailAsync(
                    recipientEmail,
                    subject,
                    body,
                    "Đỗ Tấn Du"
                );

                return Ok(new { message = $"Email đã gửi tới {recipientEmail}" });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { error = "Gửi email thất bại", details = ex.Message });
            }
        }
    }
}