using LearnApiNetCore.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace LearnApiNetCore.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body, string displayName = "");
    }
}
namespace LearnApiNetCore.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        // Tiêm (inject) IOptions<SmtpSettings> để đọc cấu hình
        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body, string displayName = "")
        {
            var message = new MimeMessage();
            string fromName = string.IsNullOrEmpty(displayName) ? _smtpSettings.Username : displayName;

            message.From.Add(new MailboxAddress(fromName, _smtpSettings.Username));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            // Thiết lập body là HTML
            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                try
                {
                    // Kết nối và xác thực
                    await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi, log ra console hoặc file
                    Console.WriteLine($"Lỗi gửi email: {ex.Message}");
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}