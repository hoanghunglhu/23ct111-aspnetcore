// Services/IEmailService.cs
using System.Threading.Tasks;

namespace BaiTapService.Services
{
    public interface IEmailService
    {
        // Sử dụng Async để phù hợp với Background Service
        Task SendEmailAsync(string to, string subject, string body, bool isHtml = false);
    }
}