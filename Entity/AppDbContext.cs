using LearnApiNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnApiNetCore.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Khai báo bảng NewsArticles
        public DbSet<NewsArticle> NewsArticles { get; set; }

        // (Bạn có thể thêm seeding data ở đây nếu muốn)
    }
}