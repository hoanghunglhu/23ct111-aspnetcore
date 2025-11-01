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
        private readonly string _smtpHost = "smtp.gmail.com";  
        private readonly int _smtpPort = 587;  
        private readonly string _smtpUsername = "quynhanhdinh164@gmail.com"; 
        private readonly string _smtpPassword = "tyze spqn zrxj jobf"; 
        private readonly string _fromEmail = "quynhanhdinh164@gmail.com";
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
                    Subject = "Thong bao tien dien!",
                    Body = "<h1>Hello em Kha, chao mung em den voi hoi dao lua</h1>",
                    IsBodyHtml = true 
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
