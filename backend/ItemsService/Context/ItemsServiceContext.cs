using ItemsService.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemsService.Context;

public class ItemsServiceContext(DbContextOptions<ItemsServiceContext> options) : DbContext(options)
{
    public DbSet<Item> Items { get; set; }
}
