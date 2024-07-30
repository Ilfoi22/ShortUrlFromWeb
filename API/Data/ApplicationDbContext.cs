using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {

    }

    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortenedUrl>()
            .HasOne(s => s.User)
            .WithMany(u => u.ShortenedUrls)
            .HasForeignKey(s => s.CreatedBy);
    }
}
