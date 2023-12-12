using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Models;

namespace ShoppingApp.Core.Data;

public class ShoppingAppContext : DbContext
{
    public DbSet<User> Users { get; set; }
}
