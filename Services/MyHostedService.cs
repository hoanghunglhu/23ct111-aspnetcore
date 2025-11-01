using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection; // Cần thêm
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging; // Cần thêm

namespace LearnApiNetCore.Services
{
    public class MyHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MyHostedService> _logger;

        // 1. Sửa Constructor: Tiêm (Inject) IServiceProvider và ILogger
        public MyHostedService(IServiceProvider serviceProvider, ILogger<MyHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background Service đang khởi chạy.");

            // Hẹn giờ chạy DoWork sau 5 giây, và lặp lại mỗi 1 giờ
            // (Bạn có thể đổi TimeSpan.FromHours(1) thành khoảng thời gian ngắn hơn để thử)
            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(5), TimeSpan.FromHours(1));
            
            return Task.CompletedTask;
        }

        // 2. Sửa DoWork thành 'async void' và thêm logic gửi mail
        private async void DoWork(object state)
        {
            _logger.LogInformation($"[{DateTime.Now}] Bắt đầu công việc gửi mail tự động...");

            // 3. Tạo một 'scope' mới để lấy các dịch vụ
            // Đây là cách làm đúng khi dùng service (như IEmailService) trong Hosted Service
            using (var scope = _serviceProvider.CreateScope()) 
            {
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                try
                {
                    // 4. Định nghĩa nội dung HTML
                    string htmlContent = @"
                        <html>
                        <body style='font-family: Arial, sans-serif;'>
                            <h1 style='color: #005a9e;'>Xin chào!</h1>
                            <p>Đây là email tự động gửi từ <strong>Background Service .NET 6</strong>.</p>
                            <p>Thời gian gửi: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + @"</p>
                        </body>
                        </html>";

                    // 5. Gọi dịch vụ gửi mail
                    await emailService.SendEmailAsync(
                        "dia-chi-nguoi-nhan@example.com", // <-- Thay đổi email người nhận
                        "Thông báo tự động từ .NET 6 Service", 
                        htmlContent
                    );
                    
                    _logger.LogInformation("Gửi mail tự động thành công!");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Gặp lỗi khi đang gửi mail tự động.");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background Service đang dừng...");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}