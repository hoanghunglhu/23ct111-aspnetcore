using Microsoft.EntityFrameworkCore;
using LearnApiNetCore.Models;

namespace LearnApiNetCore.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> users { get; set; }
        
    }
}
