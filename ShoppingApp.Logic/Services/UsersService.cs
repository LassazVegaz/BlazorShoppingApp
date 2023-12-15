using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Data;
using ShoppingApp.Core.Models;
using ShoppingApp.Core.Services;

namespace ShoppingApp.Logic.Services;

public class UsersService(IDbContextFactory<ShoppingAppContext> contextFactory) : IUsersService
{
    private readonly IDbContextFactory<ShoppingAppContext> _contextFactory = contextFactory;

    public async Task<User> CreateUser(User newUser)
    {
        newUser.Id = 0;

        using var _context = _contextFactory.CreateDbContext();
        await _context.Users.AddAsync(newUser);

        // if the email already exists, an exception will be thrown from the DB side
        _context.SaveChanges();

        return newUser;
    }

    public Task<bool> EmailExists(string email)
    {
        using var _context = _contextFactory.CreateDbContext();
        return _context.Users.AnyAsync(u => u.Email == email);
    }
}