using System;
using System.ComponentModel.DataAnnotations;

namespace LearnApiNetCore.Entity
{
    public class NewsArticle
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        
        public string Content { get; set; }
        
        public DateTime PublishedDate { get; set; }
    }
}
