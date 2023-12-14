using ShoppingApp.Core.Data;
using ShoppingApp.Core.Models;
using ShoppingApp.Core.Services;

namespace ShoppingApp.Logic.Services;

public class UsersService(ShoppingAppContext context) : IUsersService
{
    private readonly ShoppingAppContext _context = context;

    public User CreateUser(User newUser)
    {
        newUser.Id = 0;
        _context.Users.Add(newUser);
        _context.SaveChanges();

        return newUser;
    }

    public bool EmailExists(string email) => _context.Users.Any(u => u.Email == email);
}