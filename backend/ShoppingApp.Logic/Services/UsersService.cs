using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Data;
using ShoppingApp.Core.Services;

namespace ShoppingApp.Logic.Services;

public class UsersService(ShoppingAppContext contextFactory) : IUsersService
{
    private readonly ShoppingAppContext _context = contextFactory;

    public async Task<bool> EmailExists(string email) => await _context.Users.AnyAsync(u => u.Email == email);
}