namespace PurchaseService.Core;

public interface IPurchaseManager
{
    public Task Purchase(int userId, int itemId);
}
