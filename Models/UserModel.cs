namespace LearnApiNetCore.Models
{
    public class UserModel
    {
        public string name { get; set; } = null!;
        public string phone { get; set; } = null!;
        public string email { get; set; } = null!;
        public string address { get; set; } = null!;

        // === THÊM 2 DÒNG NÀY VÀO ===
        public string gender { get; set; } = null!; 
        public DateTime birthday { get; set; } // Kiểu DateTime thường không cần = null!
        // ===========================
    }
}