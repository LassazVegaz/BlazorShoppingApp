using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurchaseService.Core;
using TrendingApp.Packages.Contracts.Sagas.Order;

namespace PurchaseService;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PurchaseController(IPurchaseManager purchaseManager, IBus bus) : ControllerBase
{
    private readonly IPurchaseManager _purchaseManager = purchaseManager;
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
