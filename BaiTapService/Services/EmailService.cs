// Services/EmailService.cs
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BaiTapService.Services
{
    // Cấu trúc để đọc cài đặt SMTP từ appsettings.json
    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public string FromEmail { get; set; }
    }

    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IConfiguration config)
        {
            // Lấy cấu hình SMTP
            _smtpSettings = config.GetSection("SmtpSettings").Get<SmtpSettings>();
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false)
        {
            if (_smtpSettings == null || string.IsNullOrEmpty(_smtpSettings.Host))
            {
                // Xử lý nếu cấu hình không tồn tại
                throw new InvalidOperationException("Cấu hình SMTP không được tìm thấy.");
            }

            var message = new MailMessage(_smtpSettings.FromEmail, to, subject, body)
            {
                IsBodyHtml = isHtml
            };

            using (var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
            {
                // *** PHẦN QUAN TRỌNG ĐỂ KHẮC PHỤC LỖI 5.7.0 ***
                client.EnableSsl = _smtpSettings.EnableSsl;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
                
                try
                {
                    await client.SendMailAsync(message);
                    Console.WriteLine($"Gửi email thành công tới: {to}");
                }
                catch (SmtpException ex)
                {
                    // Ném lại lỗi để Background Service ghi nhận
                    throw new Exception($"Lỗi gửi SMTP ({ex.StatusCode}): {ex.Message}");
                }
            }
        }
    }
}