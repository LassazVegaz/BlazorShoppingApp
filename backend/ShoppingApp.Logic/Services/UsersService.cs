using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Data;
using ShoppingApp.Core.Models;
using ShoppingApp.Core.Services;

namespace ShoppingApp.Logic.Services;

public class UsersService(ShoppingAppContext contextFactory) : IUsersService
{
    private readonly ShoppingAppContext _context = contextFactory;

    public async Task<User> CreateUser(User newUser)
    {
        newUser.Id = 0;
        newUser.Email = newUser.Email.ToLower();
        newUser.Gender = newUser.Gender.ToLower();

        await _context.Users.AddAsync(newUser);

        // if the email already exists, an exception will be thrown from the DB side
        _context.SaveChanges();

        return newUser;
    }

    public async Task<bool> EmailExists(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
}