using System; // Bổ sung: Cần thiết cho Exception
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks; // Bổ sung: Cần thiết cho Task và async/await
using Microsoft.Extensions.Logging; // Bổ sung: Cần thiết cho ILogger
using Microsoft.Extensions.Options;
using LearnApiNetCore.Services; // Cần thiết để tham chiếu IEmailService và SmtpSettings

namespace LearnApiNetCore.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly ILogger<EmailService> _logger; // Khai báo ILogger

        // Dependency Injection: Nhận SmtpSettings và ILogger
        public EmailService(IOptions<SmtpSettings> smtpSettings, ILogger<EmailService> logger) // Thêm ILogger
        {
            _smtpSettings = smtpSettings.Value;
            _logger = logger; // Khởi tạo ILogger
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true)
        {
            // Kiểm tra cấu hình bắt buộc
            if (string.IsNullOrEmpty(_smtpSettings.Host) || string.IsNullOrEmpty(_smtpSettings.Username))
            {
                _logger.LogError("SMTP configuration is missing or incomplete. Cannot send email."); // Dùng ILogger
                return false;
            }

            var message = new MailMessage
            {
                From = new MailAddress(_smtpSettings.SenderEmail, _smtpSettings.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };
            message.To.Add(toEmail);

            try
            {
                using (var smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
                {
                    smtpClient.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
                    smtpClient.EnableSsl = _smtpSettings.EnableSsl;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Timeout = 10000; 

                    // 3. Gửi thư bất đồng bộ
                    await smtpClient.SendMailAsync(message);
                    _logger.LogInformation("✅ Email sent successfully to {ToEmail}.", toEmail); // Dùng ILogger
                    return true;
                }
            }
            catch (SmtpException ex)
            {
                _logger.LogError(ex, "❌ SMTP error when sending email to {ToEmail}. Status Code: {StatusCode}", toEmail, ex.StatusCode);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ General error when sending email to {ToEmail}.", toEmail);
                return false;
            }
            finally
            {
                // Đảm bảo giải phóng tài nguyên MailMessage sau khi gửi
                message.Dispose();
            }
        }
    }
}