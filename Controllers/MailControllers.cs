using namespace LearnApiNetCore.Services
using Microsoft.AspNetCore.Mvc;

namespace LearnApiNetCore.Services

{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase // Đổi tên từ MailControllers thành MailController
    {
        private readonly IEmailService _emailService;

        // Constructor Injection
        public MailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-test")]
        public async Task<IActionResult> SendTestEmail()
        {
            try
            {
                string subject = "Email Test từ ASP.NET Core";
                string body = @"
                    <h1>Xin chào!</h1>
                    <p>Đây là email được gửi tự động từ ứng dụng ASP.NET Core.</p>
                    <p style='color: blue;'>Test thành công!</p>";

                await _emailService.SendEmailAsync(
                    "email-nguoi-nhan@gmail.com", // << THAY ĐỔI EMAIL NGƯỜI NHẬN
                    subject,
                    body,
                    "Tên Hiển Thị Của Bạn" // Tên hiển thị khi gửi
                );

                return Ok(new { message = "Email đã được gửi thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Gửi email thất bại", details = ex.Message });
            }
        }
    }
}