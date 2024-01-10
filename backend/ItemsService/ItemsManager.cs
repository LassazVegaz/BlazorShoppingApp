using ItemsService.Core;
using MassTransit;
using TrendingApp.Packages.Contracts;

namespace ItemsService;

public class ItemsManager(ItemsServiceContext context, IBus bus) : IItemsManager
{
    private readonly ItemsServiceContext _context = context;
    private readonly IBus _bus = bus;


    public async Task<Item> CreateItem(Item item)
    {
        item.Id = 0;

        _context.Items.Add(item);
        await _context.SaveChangesAsync();

        await _bus.Publish<ItemCreated>(item);

        return item;
    }

    public IAsyncEnumerable<Item> GetItems() => _context.Items.AsAsyncEnumerable();
}
