using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LearnApiNetCore.Services
{
    public class EmailService
    {
        // THAY THẾ CHÍNH XÁC EMAIL CỦA BẠN DÙNG ĐỂ GỬI
        private const string SenderEmail = "datvuthanhdat22@gmail.com"; 
        
        // ĐẶT MẬT KHẨU ỨNG DỤNG CỦA BẠN VÀO ĐÂY
        private const string AppPassword = "dpwn yhpb hrwi jays"; 
        
        private const string SmtpHost = "smtp.gmail.com";
        private const int SmtpPort = 587;

        public async Task<bool> SendScheduledEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                // 1. KHỞI TẠO NỘI DUNG THƯ
                using (MailMessage message = new MailMessage(SenderEmail, toEmail)) 
                {
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true; // Gửi HTML

                using (SmtpClient smtpClient = new SmtpClient(SmtpHost))
                    {
                        smtpClient.Port = SmtpPort;
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                
                        // SỬ DỤNG EMAIL VÀ APP PASSWORD ĐỂ XÁC THỰC
                        smtpClient.Credentials = new NetworkCredential(SenderEmail, AppPassword); 
                
                        // 2. SỬ DỤNG AWAIT ĐỂ GỬI MAIL
                        await smtpClient.SendMailAsync(message); // <-- PHẢI CÓ LỆNH NÀY
                    }
                }
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Gửi mail thành công đến {toEmail}.");
                return true;
            }
    catch (Exception ex) // <-- Đã được sử dụng
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] LỖI GỬI MAIL: {ex.Message}");
        return false; // Phải return false nếu lỗi
    }
}
    }
}