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
            string fromAddress = "thanhnam14112005@gmail.com";
            string toAddress = "khatong072@gmail.com";
            string password = "scwp xims mhuv mzai";
            string subject = "Test HTML Email";
            string body = @"
<html>
  <body>
    <div style='text-align:center; font-size:40px;'>
      <span style='color:red;'>❤️❤️❤️</span><br/>
      <span style='color:red;'>❤️&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;❤️</span><br/>
      <span style='color:red;'>&nbsp;&nbsp;❤️</span>
    </div>
    <div style='text-align:center; font-size:20px; color:#d6336c;'>
      Gửi bạn một trái tim!
    </div>
  </body>
</html>";


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
