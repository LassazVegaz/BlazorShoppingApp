using PurchaseService.Models;

namespace PurchaseService.Core;

public interface IItemsManager
{
    Task<Item?> GetItem(int id);
}
