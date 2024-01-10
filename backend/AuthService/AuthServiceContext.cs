using Microsoft.EntityFrameworkCore;

namespace AuthService;

public class AuthServiceContext(DbContextOptions<AuthServiceContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>(e =>
        {
            e.Property(u => u.Id).ValueGeneratedNever();
            e.HasIndex(u => u.Email).IsUnique();
        });
    }
}
