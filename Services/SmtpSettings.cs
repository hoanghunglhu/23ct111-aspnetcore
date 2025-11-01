namespace LearnApiNetCore.Services
{
    // Class này dùng để ánh xạ (map) cấu hình từ file appsettings.json
    public class SmtpSettings
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool EnableSsl { get; set; } = true;
        public string SenderEmail { get; set; } = string.Empty;
        public string SenderName { get; set; } = "Hệ thống API"; // Tên hiển thị người gửi
    }
}