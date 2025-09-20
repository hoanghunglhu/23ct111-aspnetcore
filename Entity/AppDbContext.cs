using Microsoft.EntityFrameworkCore;

namespace LearnApiNetCore.Entity;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options)


  public DbSet<User> Users { get; set; }
}