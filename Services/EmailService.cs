//Cau hinh SMTP gui mail trong C#
// Cau hinh background service tu dong gui mail HTML sau moi 10min
//bndq nkle fwdk qbmr
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
        private readonly string _smtpUsername = "tuho6418@gmail.com"; 
        private readonly string _smtpPassword = "bndq nkle fwdk qbmr"; 
        private readonly string _fromEmail = "tuho6418@gmail.com"; 
        private readonly string _toEmail = "quynhanhdinh072@gmail.com";  

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
                    Subject = "Information",
                    Body = "<h1>Email to someone.</h1>",
                    IsBodyHtml = true  // This is important to indicate the email contains HTML
                };
                mailMessage.To.Add(_toEmail);

                var smtpClient = new SmtpClient(_smtpHost)
                {
                    Port = _smtpPort,
                    Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                    EnableSsl = true
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
