using ItemsService.Models;

namespace ItemsService.Core;

public interface IItemsService
{
    /// <summary>
    /// Item Id will be replaced by an auto-generated Id
    /// </summary>
    Task<Item> CreateItem(Item item);

    /// <summary>
    /// Get all items
    /// </summary>
    IAsyncEnumerable<Item> GetItems();
}
