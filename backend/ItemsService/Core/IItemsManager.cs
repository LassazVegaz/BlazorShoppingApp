namespace ItemsService.Core;

public interface IItemsManager
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
