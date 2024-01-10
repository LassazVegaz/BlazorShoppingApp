using PurchaseService.Core;
using PurchaseService.Models;

namespace PurchaseService.Managers;

public class UsersManager(PurchaseServiceContext context) : IUsersManager
{
    private readonly PurchaseServiceContext _context = context;


    public async Task<User?> GetUser(int userId) => await _context.Users.FindAsync(userId);
}
