namespace NewsApi.Models
{
    public class NewsItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime PublishedAt { get; set; }
    }
}
