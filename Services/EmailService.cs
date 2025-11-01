using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;

namespace LearnAspNetCore.Services
{
    public class EmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string htmlMessage)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_smtpSettings.User));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_smtpSettings.User, _smtpSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }

    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
