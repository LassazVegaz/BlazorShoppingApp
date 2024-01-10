using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TrendingApp.Packages.Contracts;
using TrendingApp.Packages.Exceptions;
using UsersService.Core.Data;
using UsersService.Core.Models;
using UsersService.Core.Options;
using UsersService.Core.Parameters;
using UsersService.Core.Services;
using BC = BCrypt.Net.BCrypt;

namespace UsersService.Logic.Services;

public class UsersService(IOptions<UserOptions> userOptions, ShoppingAppContext contextFactory, IBus bus) : IUsersService
{
    private readonly UserOptions _userOptions = userOptions.Value;
    private readonly ShoppingAppContext _context = contextFactory;
    private readonly IBus _bus = bus;


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
        newUser.Credits = _userOptions.InitialCredits;
        newUser.Email = newUser.Email.ToLower();
        newUser.Gender = newUser.Gender.ToLower();

        newUser.Password = BC.HashPassword(newUser.Password);

        await _context.Users.AddAsync(newUser);

        // if the email already exists, an exception will be thrown from the DB side
        _context.SaveChanges();

        await _bus.Publish<UserCreated>(newUser);

        newUser.Password = string.Empty;
        return newUser;
    }

    public async Task<User> UpdateUser(int id, UpdateUser updatedUser)
    {
        var user = _context.Users.Find(id) ?? throw new NotFoundException("User not found");

        if (updatedUser.Email != null && !updatedUser.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase))
        {
            var emailUpdateDiff = DateTime.Now.Date - user.EmailUpdatedOn?.ToDateTime(new TimeOnly(0));
            if (emailUpdateDiff.HasValue && emailUpdateDiff.Value.TotalDays < _userOptions.EmailUpdateIntervalInDays)
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

    public async Task<bool> ChangePassword(int id, string oldPassword, string newPassword)
    {
        var user = await _context.Users.FindAsync(id) ?? throw new NotFoundException("User not found");

        if (!BC.Verify(oldPassword, user.Password)) return false;

        user.Password = BC.HashPassword(newPassword);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> EmailExists(string email) => await _context.Users.AnyAsync(u => u.Email == email.ToLower());

    public async Task DeductCredits(int userId, double credits)
    {
        var user = await _context.Users.FindAsync(userId) ?? throw new NotFoundException("User not found");

        if (user.Credits < credits) throw new BadArgumentsException("Not enough credits");

        user.Credits -= credits;
        await _context.SaveChangesAsync();
    }
}