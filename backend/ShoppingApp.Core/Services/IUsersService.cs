using ShoppingApp.Core.Models;
using ShoppingApp.Core.Parameters;

namespace ShoppingApp.Core.Services;

public interface IUsersService
{
    /// <summary>
    /// In the returned user object, password is an empty string
    /// </summary>
    Task<User?> GetUserById(int id);

    Task<User> CreateUser(User newUser);

    /// <summary>
    /// Update user using non-null properties of
    /// <see cref="ShoppingApp.Core.Parameters.UpdateUser">UpdateUser</see>. Currently all the properties of user
    /// are non-null.
    /// </summary>
    Task<User> UpdateUser(int id, UpdateUser updatedUser);

    /// <summary>
    /// Check if the email exists in the database
    /// </summary>
    Task<bool> EmailExists(string email);
}
