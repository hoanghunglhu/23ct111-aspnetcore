using Microsoft.AspNetCore.Mvc;

public class MailController : Controller
{
    private readonly IEmailService _emailService;

    public MailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpGet("sendmail")]
    public async Task<IActionResult> SendMail()
    {
        await _emailService.SendEmailAsync(
            "datvuthanhdat22@gmail.com",
            "Xin chào!",
            "<h3>Đây là email test từ ASP.NET Core 🚀</h3>"
        );

        return Ok("Email đã được gửi!");
    }
}