using ShoppingApp.Core.Models;

namespace ShoppingApp.Core.Services;

public interface IUsersService
{
    User CreateUser(User newUser);

    /// <summary>
    /// Check if the email exists in the database
    /// </summary>
    bool EmailExists(string email);
}
