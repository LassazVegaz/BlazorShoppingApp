using PurchaseService.Models;

namespace PurchaseService.Core;

public interface IUsersManager
{
    Task<User?> GetUser(int userId);
}
