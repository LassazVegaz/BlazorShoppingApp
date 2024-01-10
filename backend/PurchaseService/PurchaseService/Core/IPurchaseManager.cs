namespace PurchaseService.Core;

public interface IPurchaseManager
{
    public Task Purchase(int userId, int itemId);

    /// <summary>
    /// This will remove the purchase record and add credits back to user.
    /// <br />
    /// Note: This will throw errors if the purchase record does not exist.
    /// </summary>
    public Task RevertPurchase(int userId, int itemId);

    /// <summary>
    /// Get items purchased by a user
    /// </summary>
    /// <returns>IDs of the items user has purchased</returns>
    public Task<IEnumerable<int>> GetPurchasedItems(int userId);

    /// <summary>
    /// Check if user has purchased an item
    /// </summary>
    public Task<bool> IsPurchased(int userId, int itemId);
}
