//Cau hinh SMTP gui mail trong C#
//Cau hinh background servicetu dong gui HTML sau moi 10min
//tyze spqn zrxj jobf
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
        private readonly string _smtpHost = "smtp.gmail.com";  // Replace with your SMTP host
        private readonly int _smtpPort = 587;  // Typically 587 for TLS, change based on your SMTP server
        private readonly string _smtpUsername = "quynhanhdinh164@gmail.com";  // Your email
        private readonly string _smtpPassword = "tyze spqn zrxj jobf";  // Your email password
        private readonly string _fromEmail = "quynhanhdinh164@gmail.com";  // The "from" email address
        private readonly string _toEmail = "tuho6418@gmail.com";  // The recipient's email address

        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Set the timer to call the SendEmail method every 10 minutes (600000 ms)
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
                    Subject = "Test Email Subject",
                    Body = "<h1>This is an HTML email sent from the background service!</h1>",
                    IsBodyHtml = true  // This is important to indicate the email contains HTML
                };
                mailMessage.To.Add(_toEmail);

                var smtpClient = new SmtpClient(_smtpHost)
                {
                    Port = _smtpPort,
                    Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                    EnableSsl = true  // Use SSL for security
                };

                smtpClient.Send(mailMessage);
                Console.WriteLine($"[{DateTime.Now}] Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now}] Failed to send email: {ex.Message}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop the timer when the service is stopped
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
