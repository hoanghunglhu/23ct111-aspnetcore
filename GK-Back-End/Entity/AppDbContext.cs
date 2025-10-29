using Microsoft.EntityFrameworkCore;
using StudentClassApi.Models;

namespace StudentClassApi.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
