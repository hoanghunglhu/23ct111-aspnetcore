namespace LearnApiNetCore.Models
{
    public class NewsArticle
    {
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public string content { get; set; } = string.Empty;
        public DateTime publishedDate { get; set; }
    }
}