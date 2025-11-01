// Đảm bảo namespace này khớp với các file Services khác của bạn
namespace LearnApiNetCore.Services 
{
    public class SmtpSettings
    {
        // Tôi đã thêm = null!; để sửa luôn lỗi cảnh báo CS8618
        public string Server { get; set; } = null!;
        public int Port { get; set; }
        public string From { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string gender { get; set; } = null!;
    }
}