using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ShoppingApp.Core.Data;

public class ShoppingAppContext(DbContextOptions<ShoppingAppContext> options) : IdentityDbContext(options)
{
}
