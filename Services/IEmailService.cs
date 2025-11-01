using System.Threading.Tasks;

namespace LearnApiNetCore.Services
{
    public interface IEmailService
    {
        // Phương thức gửi email đơn giản
        Task<bool> SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true);
    }
}