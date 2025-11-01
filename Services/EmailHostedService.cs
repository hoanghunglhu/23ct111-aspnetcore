using Microsoft.Extensions.Hosting;
using System;
using System.Net.Mail;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace LearnApiNetCore.Services
{
    public class EmailHostedService : IHostedService, IDisposable
    {
        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SendEmail, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
            return Task.CompletedTask;
        }

        private void SendEmail(object state)
        {
            string fromAddress = "apps@xxxx.com";
            string toAddress = "receiver@xxxx.com";
            string password = "your-app-password";
            string subject = "Test HTML Email";
            string body = "<h1>Nội dung HTML gửi tự động</h1>";

            using(var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromAddress, password);

                using(var message = new MailMessage(fromAddress, toAddress, subject, body))
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
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
