using System.ComponentModel.DataAnnotations;

namespace LearnApiNetCore.Entity
{
    public class NewsArticle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = null!; // <--- SỬA Ở ĐÂY

        public string Content { get; set; } = null!; // <--- SỬA Ở ĐÂY

        public DateTime PublishedDate { get; set; }
    }
}