namespace PurchaseService.Core;

public interface IPurchaseManager
{
    public Task Purchase(int userId, int itemId);

    /// <summary>
    /// Check if user has purchased an item
    /// </summary>
    public Task<bool> IsPurchased(int userId, int itemId);
}
