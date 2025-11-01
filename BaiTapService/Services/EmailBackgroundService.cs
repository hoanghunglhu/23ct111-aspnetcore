// Services/EmailBackgroundService.cs (Không cần sửa)

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BaiTapService.Services
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;

        public EmailBackgroundService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _services.CreateScope();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                try
                {
                    await emailService.SendEmailAsync( // Đúng: đã sử dụng SendEmailAsync
                        to: "phihung275corday@gmail.com",
                        subject: "Background Email - Test",
                        body: $"<h3>Background service đang chạy!</h3><p>Thời gian: {DateTime.Now}</p>",
                        isHtml: true
                    );
                }
                catch (Exception ex)
                {
                    // Ghi nhận lỗi trong Console
                    Console.WriteLine($"Lỗi gửi email Background: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Gửi mỗi 1 phút
            }
        }
    }
}