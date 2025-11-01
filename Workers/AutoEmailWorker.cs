using LearnApiNetCore.Services;

namespace LearnApiNetCore.Workers
{
    // BackgroundService là lớp cơ sở tiện lợi cho IHostedService
    public class AutoEmailWorker : BackgroundService
    {
        private readonly ILogger<AutoEmailWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public AutoEmailWorker(ILogger<AutoEmailWorker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Auto Email Worker đang chạy.");

            // Vòng lặp chạy liên tục cho đến khi ứng dụng bị dừng
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Bắt đầu công việc gửi email định kỳ.");

                    // *** RẤT QUAN TRỌNG: Tạo một Scope mới ***
                    // Background service là Singleton, nhưng IEmailService là Transient
                    // Chúng ta phải tạo scope mới để lấy instance IEmailService
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                        // Chuẩn bị nội dung email HTML
                        string subject = "Báo cáo tự động (10 phút)";
                        string body = @"
                            <h1>Xin chào!</h1>
                            <p>Đây là email được gửi tự động mỗi 10 phút từ Background Service.</p>
                            <p style='color: green; font-weight: bold;'>Thời gian gửi: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + @"</p>
                            <p><i>Cảm ơn đã sử dụng dịch vụ.</i></p>";
                        
                        string recipientEmail = "email-nguoi-nhan@example.com"; // << THAY EMAIL NGƯỜI NHẬN

                        // Gửi email
                        await emailService.SendEmailAsync(recipientEmail, subject, body, "Hệ Thống Tự Động");
                    }

                    _logger.LogInformation("Hoàn thành công việc gửi email.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Đã xảy ra lỗi trong Auto Email Worker.");
                }

                // Chờ 10 phút cho lần chạy tiếp theo
                _logger.LogInformation("Chờ 10 phút cho lần chạy tiếp theo...");
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }

            _logger.LogWarning("Auto Email Worker đang dừng.");
        }
    }
}