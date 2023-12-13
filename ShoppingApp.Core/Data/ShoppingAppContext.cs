using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Models;

namespace ShoppingApp.Core.Data;

public class ShoppingAppContext(DbContextOptions<ShoppingAppContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_unique_email")
                  .IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}
