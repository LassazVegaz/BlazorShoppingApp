using ShoppingApp.Core.Models;

namespace ShoppingApp.Core.Services;

public interface IUsersService
{
    Task<User> CreateUser(User newUser);

    Task<User?> GetUserById(int id);

    /// <summary>
    /// Check if the email exists in the database
    /// </summary>
    Task<bool> EmailExists(string email);
}
