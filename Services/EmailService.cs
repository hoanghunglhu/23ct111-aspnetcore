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
        private readonly string _smtpHost = "smtp.gmail.com";  
        private readonly int _smtpPort = 587;  
        private readonly string _smtpUsername = "nguyenbinhan2707@gmail.com";  
        private readonly string _smtpPassword = "ibfs jjfi ybsr wzrf"; 
        private readonly string _fromEmail = "nguyenbinhan2707@gmail.com";  
        private readonly string _toEmail = "khatong072@gmail.com"; 

        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
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
                    Subject = "Important Security Alert: Potential Risks and Vulnerabilities on iPhone Devices",
                    Body = "<h1>Dear Tong Anh Kha</h1>",
                    IsBodyHtml = true 
                };
                mailMessage.To.Add(_toEmail);

                var smtpClient = new SmtpClient(_smtpHost)
                {
                    Port = _smtpPort,
                    Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                    EnableSsl = true //SSL
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
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}