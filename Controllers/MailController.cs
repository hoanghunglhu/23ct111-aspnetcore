using LearnApiNetCore.Services; // Namespace của IEmailService
using Microsoft.AspNetCore.Mvc;

namespace LearnApiNetCore.Controllers // Namespace cho Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase // Tên class (số ít) khớp với tên file
    {
        private readonly IEmailService _emailService;

        // Tiêm (inject) IEmailService
        public MailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-test")]
        public async Task<IActionResult> SendTestEmail([FromQuery] string recipientEmail)
        {
            try
            {
                string subject = "Email Test từ ASP.NET Core";
                string body = "<h1>Test thành công!</h1><p>Email này được gửi từ API.</p>";

                await _emailService.SendEmailAsync(
                    recipientEmail, // Gửi đến email bạn nhập
                    subject,
                    body,
                    "Hệ Thống LearnApiNetCore"
                );

                return Ok(new { message = $"Email đã gửi tới {recipientEmail}" });
            }
            catch (Exception ex)
            {
                // Trả về lỗi 500 nếu có vấn đề
                return StatusCode(500, new { error = "Gửi email thất bại", details = ex.Message });
            }
        }
    }
}