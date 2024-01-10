using PurchaseService.Core;
using PurchaseService.Models;

namespace PurchaseService.Managers;

public class ItemsManager(PurchaseServiceContext context) : IItemsManager
{
    private readonly PurchaseServiceContext _context = context;

    public async Task<Item?> GetItem(int id) => await _context.Items.FindAsync(id);
}
