using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace LearnApiNetCore.Services
{
    public class EmailHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private void SendEmail()
        {
            string fromAddress = "thanhnam14112005@gmail.com";
            string toAddress = "bemo14112005@gmail.com";
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
            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromAddress, password);
                using (var message = new MailMessage(fromAddress, toAddress, subject, body))
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
            try
            {
                SendEmail();
                Console.WriteLine($"[{DateTime.Now}] Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now}] Error sending email: {ex.Message}");
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
