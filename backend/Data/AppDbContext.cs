using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Transfer> Transfers => Set<Transfer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.FromUser)
            .WithMany()
            .HasForeignKey(t => t.FromUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.ToUser)
            .WithMany()
            .HasForeignKey(t => t.ToUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
