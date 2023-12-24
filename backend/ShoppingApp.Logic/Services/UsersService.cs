using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Data;
using ShoppingApp.Core.Services;

namespace ShoppingApp.Logic.Services;

public class UsersService(ShoppingAppContext contextFactory) : IUsersService
{
    private readonly ShoppingAppContext _context = contextFactory;

    public async Task<IdentityUser> CreateUser(IdentityUser newUser)
    {
        await _context.Users.AddAsync(newUser);

        _context.SaveChanges();

        return newUser;
    }

    public async Task<bool> EmailExists(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
}