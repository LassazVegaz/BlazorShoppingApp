using Microsoft.EntityFrameworkCore;
using PurchaseService.Models;

namespace PurchaseService;

public class PurchaseServiceContext(DbContextOptions<PurchaseServiceContext> options)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Item> Items { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.Property(p => p.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Item>(e =>
        {
            e.Property(p => p.Id).ValueGeneratedNever();
        });

        base.OnModelCreating(modelBuilder);
    }
}
