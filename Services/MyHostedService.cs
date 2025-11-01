namespace LearnApiNetCore.Services
{
    public class MyHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly EmailService _emailService;

        public MyHostedService(EmailService emailService)
        {
            _emailService = emailService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Thiết lập Timer: Bắt đầu ngay (TimeSpan.Zero), lặp lại sau mỗi 10 phút.
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Dịch vụ gửi mail nền đã khởi động. Lặp lại mỗi 10 phút.");
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            //Console.WriteLine($"[{DateTime.Now}] Welcome to ASP.NETCore API...");
            // Khởi tạo và sử dụng EmailService ngay tại đây
            //var emailService = new EmailService();
            // Thay đổi thông tin người nhận thực tế của bạn
            //emailService.SendEmailAsync("nguoi_nhan_test@example.com", "Báo cáo định kỳ", $"Thời gian: {DateTime.Now}").Wait();
            string recipient = "hoanghunglhu@gmail.com"; // <-- THAY THẾ NGƯỜI NHẬN
            string subject = $"BÁO CÁO TỰ ĐỘNG - {DateTime.Now:dd/MM/yyyy HH:mm}";
            string htmlBody = $@"
                <html>
                    <body>
                        <h1 style='color: green;'>Báo cáo định kỳ từ ASP.NET Core</h1>
                        <p><strong>Thời gian thực hiện:</strong> {DateTime.Now:dd/MM/yyyy HH:mm:ss}</p>
                        <p>Tác vụ nền đã chạy thành công. Email này được gửi tự động mỗi 10 phút.</p>
                        <hr>
                        <small>Sử dụng Mật khẩu ứng dụng Google.</small>
                    </body>
                </html>";

            await _emailService.SendScheduledEmailAsync(recipient, subject, htmlBody);
        
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //_timer?.Change(Timeout.Infinite, 0);
            //return Task.CompletedTask;
            _timer?.Change(Timeout.Infinite, 0);
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Dich vu gui email nen da dung.");
            return Task.CompletedTask;
        }

        public void Dispose() => _timer?.Dispose();
    }

}