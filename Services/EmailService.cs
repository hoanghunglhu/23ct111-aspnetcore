// cau hinh SMTP gui mail trong C#
// cau hinh background service tu dong gui mail HTML
// SAU MOI 10P
using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace LearnApiNetCore.Services
{
    public class EmailService : IHostedService, IDisposable
    {
        private readonly string _smtpHost = " smtp.gmail.com "; // Replace with your SMTP server
        private readonly int _smtpPort = 587; // Usually 587 for TLS, change based on your SMTP server
        private readonly string _smtpUsername = " hachieu375@gmail.com "; // Your email
        private readonly string _smtpPassword = "ngyw fach vewd klnr"; // Your email password
        private readonly string _fromEmail = "hachieu375@gmail.com "; // Email address "from"
        private readonly string _toEmail = "khatong072@gmail.com "; // Email address of recipient

        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Set timer to call SendEmail method every 10 minutes (600000 ms)
            _timer = new Timer(SendEmail, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
            return Task.CompletedTask;
        }

        private void SendEmail(object state)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail),
                    Subject="test email",
                    Body = "<h1> Chủ đề: KHẨN CẤP: [Địa chỉ Email của bạn] ĐÃ BỊ HACK - XIN ĐỪNG MỞ HOẶC TRẢ LỜI CÁC EMAIL GẦN ĐÂY/brNội dung:/brChào mọi người,/brTôi viết email này từ một địa chỉ email/phương tiện khác để thông báo rằng tài khoản email của tôi, [khatong072@gmail.com], vừa bị xâm nhập/hack.dot </h1>",
                    IsBodyHtml = true // This is important to indicate that the email contains HTML
                };
                mailMessage.To.Add(_toEmail);

                var smtpClient = new SmtpClient(_smtpHost)
                {
                    Port = _smtpPort,
                    Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                    EnableSsl = true // Use SSL for security
                };

                smtpClient.Send(mailMessage);
                Console.WriteLine($"[{DateTime.Now}] Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now}] Email failed to send: {ex.Message}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop the timer when the service stops
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
