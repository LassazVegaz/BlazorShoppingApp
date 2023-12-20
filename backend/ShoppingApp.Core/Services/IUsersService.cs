using ShoppingApp.Core.Models;

namespace ShoppingApp.Core.Services;

public interface IUsersService
{
    Task<User> CreateUser(User newUser);

    /// <summary>
    /// Check if the email exists in the database
    /// </summary>
    Task<bool> EmailExists(string email);
}
