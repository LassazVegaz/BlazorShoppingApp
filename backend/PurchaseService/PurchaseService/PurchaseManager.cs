using Microsoft.EntityFrameworkCore;
using PurchaseService.Core;

namespace PurchaseService;

public class PurchaseManager(PurchaseServiceContext context) : IPurchaseManager
{
    private readonly PurchaseServiceContext _context = context;

    public async Task<IEnumerable<int>> GetPurchasedItems(int userId)
        => await _context.Users.Where(u => u.Id == userId)
                               .Include(u => u.Items)
                               .Select(u => u.Items.Select(i => i.Id))
                               .FirstOrDefaultAsync()
        ?? throw new ArgumentException($"User with id {userId} does not exist");

    public async Task<bool> IsPurchased(int userId, int itemId)
        => await _context.Users.AnyAsync(u => u.Id == userId && u.Items.Any(i => i.Id == itemId));

    public async Task Purchase(int userId, int itemId)
    {
        if (await IsPurchased(userId, itemId))
            throw new ArgumentException($"User with id {userId} has already purchased item with id {itemId}");

        var user = await _context.Users.FindAsync(userId) ?? throw new ArgumentException($"User with id {userId} does not exist");
        var item = await _context.Items.FindAsync(itemId) ?? throw new ArgumentException($"Item with id {itemId} does not exist");

        if (user.Credits < item.Price)
            throw new ArgumentException($"User with id {userId} does not have enough money to buy item with id {itemId}");

        user.Credits -= item.Price;

        user.Items.Add(item);

        await _context.SaveChangesAsync();
    }
}
