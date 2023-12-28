using ShoppingApp.Core.Models;

namespace ShoppingApp.Core.Services;

public interface IUsersService
{
    Task<User> CreateUser(User newUser);

    /// <summary>
    /// In the returned user object, password is an empty string
    /// </summary>
    Task<User?> GetUserById(int id);

    /// <summary>
    /// Check if the email exists in the database
    /// </summary>
    Task<bool> EmailExists(string email);
}
