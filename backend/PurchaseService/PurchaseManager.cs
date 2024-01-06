using PurchaseService.Core;

namespace PurchaseService;

public class PurchaseManager(PurchaseServiceContext context) : IPurchaseManager
{
    private readonly PurchaseServiceContext _context = context;

    public async Task Purchase(int userId, int itemId)
    {
        var user = await _context.Users.FindAsync(userId) ?? throw new ArgumentException($"User with id {userId} does not exist");
        var item = await _context.Items.FindAsync(itemId) ?? throw new ArgumentException($"Item with id {itemId} does not exist");

        if (user.Credits < item.Price)
            throw new ArgumentException($"User with id {userId} does not have enough money to buy item with id {itemId}");

        user.Credits -= item.Price;

        user.Items.Add(item);

        await _context.SaveChangesAsync();
    }
}
