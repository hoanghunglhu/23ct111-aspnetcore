using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace LearnApiNetCore.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _settings;

        // Dùng IOptions để tiêm (inject) SmtpSettings vào
        public EmailService(IOptions<SmtpSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
        {
            // 1. Tạo đối tượng MailMessage
            var message = new MailMessage();
            message.From = new MailAddress(_settings.From);
            message.To.Add(new MailAddress(toEmail));
            message.Subject = subject;
            message.Body = htmlBody; 
            
            // ⭐️ Đây là phần quan trọng để gửi nội dung HTML
            message.IsBodyHtml = true;

            // 2. Cấu hình SmtpClient
            using (var client = new SmtpClient(_settings.Server))
            {
                client.Port = _settings.Port;
                client.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
                client.EnableSsl = true; // Bật SSL (bắt buộc cho Gmail)

                // 3. Gửi mail
                await client.SendMailAsync(message);
            }
        }
    }
}