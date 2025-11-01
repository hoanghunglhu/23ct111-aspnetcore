using Microsoft.EntityFrameworkCore;
namespace LearnApiNetCore.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<NewsArticle> NewsArticles { get; set; }
        public DbSet<User> Users { get; set; }
        
    }
}