using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Models;

namespace ShoppingApp.Core.Data;

public class ShoppingAppContext(DbContextOptions<ShoppingAppContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}
