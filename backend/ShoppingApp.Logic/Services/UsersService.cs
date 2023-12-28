using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Data;
using ShoppingApp.Core.Models;
using ShoppingApp.Core.Services;
using BC = BCrypt.Net.BCrypt;

namespace ShoppingApp.Logic.Services;

public class UsersService(ShoppingAppContext contextFactory) : IUsersService
{
    private readonly ShoppingAppContext _context = contextFactory;

    public async Task<User> CreateUser(User newUser)
    {
        newUser.Id = 0;
        newUser.Email = newUser.Email.ToLower();
        newUser.Gender = newUser.Gender.ToLower();

        newUser.Password = BC.HashPassword(newUser.Password);

        await _context.Users.AddAsync(newUser);

        // if the email already exists, an exception will be thrown from the DB side
        _context.SaveChanges();

        return newUser;
    }

    public async Task<bool> EmailExists(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserById(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;

        user.Password = string.Empty;

        return user;
    }
}