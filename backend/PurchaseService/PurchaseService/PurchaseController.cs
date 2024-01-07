using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurchaseService.Core;

namespace PurchaseService;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PurchaseController(IPurchaseManager purchaseManager) : ControllerBase
{
    private readonly IPurchaseManager _purchaseManager = purchaseManager;


    [HttpGet("isPurchased")]
    public async Task<ActionResult<bool>> IsPurchased(int itemId)
    {
        var userId = int.Parse(User.Identity!.Name!);

        var isPurchased = await _purchaseManager.IsPurchased(userId, itemId);

        return Ok(isPurchased);
    }

    [HttpPost]
    public async Task<IActionResult> Purchase(int itemId)
    {
        var userId = int.Parse(User.Identity!.Name!);

        await _purchaseManager.Purchase(userId, itemId);

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
