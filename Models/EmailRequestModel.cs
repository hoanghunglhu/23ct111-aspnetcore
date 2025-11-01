using System.ComponentModel.DataAnnotations;

namespace LearnApiNetCore.Services
{
    // Model này dùng để nhận dữ liệu từ body của request HTTP POST
    public class EmailRequestModel
    {
        [Required(ErrorMessage = "Địa chỉ email nhận không được để trống.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string ToEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tiêu đề không được để trống.")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nội dung thư không được để trống.")]
        public string Body { get; set; } = string.Empty;
    }
}
