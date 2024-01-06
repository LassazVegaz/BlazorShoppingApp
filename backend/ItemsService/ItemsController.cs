using ItemsService.Core;
using Microsoft.AspNetCore.Mvc;

namespace ItemsService;

[Route("api/[controller]")]
[ApiController]
public class ItemsController(IItemsService itemsService) : ControllerBase
{
    private readonly IItemsService _itemsService = itemsService;

    [HttpPost]
    public async Task<ActionResult<Item>> CreateItem(Item item)
    {
        var createdItem = await _itemsService.CreateItem(item);
        return StatusCode(StatusCodes.Status201Created, createdItem);
    }

    [HttpGet]
    public ActionResult<IAsyncEnumerable<Item>> GetItems()
    {
        var items = _itemsService.GetItems();
        return Ok(items);
    }
}
