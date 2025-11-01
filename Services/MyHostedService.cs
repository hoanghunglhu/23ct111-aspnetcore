using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LearnApiNetCore.Services
{
    public class MyHostedService : IHostedService, IDisposable
    {
        // === SỬA 1: THÊM DẤU ? VÀO ĐÂY ===
        private Timer? _timer;
        // ==================================
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MyHostedService> _logger;

        public MyHostedService(IServiceProvider serviceProvider, ILogger<MyHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background Service đang khởi chạy.");
            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(5), TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }

        // === SỬA 2: THÊM DẤU ? VÀO ĐÂY ===
        private async void DoWork(object? state)
{
    _logger.LogInformation($"[{DateTime.Now}] Bắt đầu công việc gửi mail tự động...");

    // ... (Phần code còn lại giữ nguyên) ...  <-- BẠN ĐANG BỊ THẾ NÀY

    // BẠN CẦN BỎ COMMENT KHỐI NÀY RA
    using (var scope = _serviceProvider.CreateScope()) 
    {
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
        try
        {
            // ... (tạo htmlContent) ...

            // PHẢI CÓ DÒNG 'AWAIT' NÀY
            await emailService.SendEmailAsync(
                "dotandu2018st@gmail.com", 
                "Skibidi", 
                "Datbeo"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Lỗi gửi mail");
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