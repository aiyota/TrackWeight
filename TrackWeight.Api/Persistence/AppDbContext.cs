using Microsoft.EntityFrameworkCore;
using TrackWeight.Api.Models;

namespace TrackWeight.Api.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().Property(u => u.FirstName).IsRequired();
        modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired();
        modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique(); 
        modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired();

        modelBuilder.Entity<WeightRecord>().HasKey(u => u.Id);
        modelBuilder.Entity<WeightRecord>().Property(u => u.UserId).IsRequired();
        modelBuilder.Entity<WeightRecord>().Property(u => u.Weight).IsRequired();
        modelBuilder.Entity<WeightRecord>().Property(u => u.CreatedAt).IsRequired();
        modelBuilder.Entity<WeightRecord>()
             .HasOne<User>()
             .WithMany()
             .HasForeignKey(u => u.UserId)
             .IsRequired();
    }
}
