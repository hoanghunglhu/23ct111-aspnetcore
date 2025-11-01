using Microsoft.EntityFrameworkCore;
using LearnApiNetCore.Entity; 

namespace LearnApiNetCore.Entity 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // === SỬA 2 DÒNG NÀY ===
        public DbSet<NewsArticle> NewsArticles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        // =======================
    }
}