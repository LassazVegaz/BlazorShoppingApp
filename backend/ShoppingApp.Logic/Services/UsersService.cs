using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Data;
using ShoppingApp.Core.Services;

namespace ShoppingApp.Logic.Services;

public class UsersService(ShoppingAppContext context) : IUsersService
{
    private readonly ShoppingAppContext _context = context;

    public async Task<bool> EmailExists(string email) => await _context.Users.AnyAsync(u => u.Email == email);
}