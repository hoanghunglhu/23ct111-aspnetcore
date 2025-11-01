using Microsoft.Extensions.Hosting;

namespace LearnAspNetCore.Services
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly EmailService _emailService;

        public EmailBackgroundService(EmailService emailService)
        {
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Background service sending HTML mail...");

                await _emailService.SendEmailAsync(
                    to: "trungthong2707005@gmail.com",
                    subject: "Công an phường Tân Uyên",
                    htmlMessage: "Chiều ngày 2/11/2025 mời công dân Bùi Trung Thông lên phường giải trình vụ việc liên quan đến đường dây ma túy ở biên giới Lào Cai"
                );

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
