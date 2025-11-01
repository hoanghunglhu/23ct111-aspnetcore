using Microsoft.AspNetCore.Mvc;
using LearnApiNetCore.Services; // Dùng cho IEmailService và EmailRequestModel
using System.Threading.Tasks;
using Microsoft.Extensions.Logging; // Bổ sung: Dùng cho ILogger
using System; // Bổ sung: Dùng cho Exception
using System.ComponentModel.DataAnnotations; // Bổ sung: Dùng cho các thuộc tính model validation

namespace LearnApiNetCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailController> _logger;

        // Constructor: Inject IEmailService và ILogger
        public EmailController(IEmailService emailService, ILogger<EmailController> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint dùng để gửi email.
        /// </summary>
        [HttpPost("send")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                // Trả về lỗi nếu dữ liệu đầu vào không hợp lệ (ví dụ: thiếu email)
                return BadRequest(ModelState);
            }

            try
            {
                // Gọi dịch vụ gửi mail
                bool success = await _emailService.SendEmailAsync(
                    request.ToEmail,
                    request.Subject,
                    request.Body,
                    isHtml: true // Gửi dưới dạng HTML
                );

                if (success)
                {
                    _logger.LogInformation("Email sent successfully to {ToEmail}.", request.ToEmail);
                    return Ok(new { message = $"Email đã gửi thành công đến {request.ToEmail}" });
                }
                else
                {
                    // Lỗi gửi mail do cấu hình SMTP sai (đã được ghi log chi tiết trong EmailService)
                    _logger.LogError("Failed to send email to {ToEmail}. Check SmtpSettings configuration.", request.ToEmail);
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Gửi email thất bại. Vui lòng kiểm tra log hoặc cấu hình SMTP." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while sending email to {ToEmail}.", request.ToEmail);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Đã xảy ra lỗi không mong muốn khi xử lý yêu cầu." });
            }
        }
    }
}