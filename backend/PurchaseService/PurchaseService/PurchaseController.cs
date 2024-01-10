using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurchaseService.Core;
using TrendingApp.Packages.Contracts.Sagas.Order;

namespace PurchaseService;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PurchaseController(IBus bus, IPurchaseManager purchaseManager, IItemsManager itemsManager, IUsersManager usersManager)
    : ControllerBase
{
    private readonly IPurchaseManager _purchaseManager = purchaseManager;
    private readonly IItemsManager _itemsManager = itemsManager;
    private readonly IUsersManager _usersManager = usersManager;
    private readonly IBus _bus = bus;


    [HttpGet("isPurchased")]
    public async Task<ActionResult<bool>> IsPurchased(int itemId)
    {
        var userId = int.Parse(User.Identity!.Name!);

        var isPurchased = await _purchaseManager.IsPurchased(userId, itemId);

        return Ok(isPurchased);
    }

    [HttpPost("{itemId}")]
    public async Task<IActionResult> Purchase(int itemId)
    {
        var userId = int.Parse(User.Identity!.Name!);

        var item = await _itemsManager.GetItem(itemId);
        if (item is null) return NotFound($"Item with id {itemId} does not exist");

        var user = (await _usersManager.GetUser(userId))!;
        if (user.Credits < item.Price)
            return BadRequest($"User with id {userId} does not have enough money to buy item with id {itemId}");

        if (await _purchaseManager.IsPurchased(userId, itemId))
            return BadRequest($"User with id {userId} has already purchased (or purchasing) item with id {itemId}");

        await _bus.Publish(new UserPlacedOrder
        {
            UserId = userId,
            ItemId = itemId
        });

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<int>>> GetPurchasedItems()
    {
        var userId = int.Parse(User.Identity!.Name!);

        var purchasedItems = await _purchaseManager.GetPurchasedItems(userId);

        return Ok(purchasedItems);
    }
}
