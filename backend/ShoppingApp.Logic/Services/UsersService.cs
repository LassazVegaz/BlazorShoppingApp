using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShoppingApp.Core.Data;
using ShoppingApp.Core.Exceptions;
using ShoppingApp.Core.Models;
using ShoppingApp.Core.Options;
using ShoppingApp.Core.Parameters;
using ShoppingApp.Core.Services;
using BC = BCrypt.Net.BCrypt;

namespace ShoppingApp.Logic.Services;

public class UsersService(IOptions<UserOptions> userOptions, ShoppingAppContext contextFactory) : IUsersService
{
    private readonly ShoppingAppContext _context = contextFactory;
    private readonly UserOptions _userOptions = userOptions.Value;


    public async Task<User?> GetUserById(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;

        user.Password = string.Empty;

        return user;
    }

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

    public async Task<User> UpdateUser(int id, UpdateUser updatedUser)
    {
        var user = _context.Users.Find(id) ?? throw new NotFoundException("User not found");

        if (updatedUser.Email != null && !updatedUser.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase))
        {
            var emailCanBeUpdatedOn = user.EmailUpdatedOn?.AddDays(_userOptions.EmailUpdateIntervalInDays);
            if (user.EmailUpdatedOn.HasValue && emailCanBeUpdatedOn > DateOnly.FromDateTime(DateTime.Now))
                throw new BadArgumentsException("Email cannot be updated until the buffer time is over. Whole update operation is aborted");

            user.Email = updatedUser.Email.ToLower();
            user.EmailUpdatedOn = DateOnly.FromDateTime(DateTime.Now); // i dont care about UTC at the moment
        }

        if (updatedUser.FirstName != null) user.FirstName = updatedUser.FirstName;
        if (updatedUser.LastName != null) user.LastName = updatedUser.LastName;
        if (updatedUser.Gender != null) user.Gender = updatedUser.Gender.ToLower();
        if (updatedUser.DateOfBirth != null) user.DateOfBirth = updatedUser.DateOfBirth.Value;

        await _context.SaveChangesAsync();

        user.Password = string.Empty;
        return user;
    }

    public async Task<bool> EmailExists(string email) => await _context.Users.AnyAsync(u => u.Email == email.ToLower());
}