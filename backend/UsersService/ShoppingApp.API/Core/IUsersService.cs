using UsersService.Core.Parameters;

namespace UsersService.Core;

public interface IUsersService
{
    /// <summary>
    /// In the returned user object, password is an empty string
    /// </summary>
    Task<User?> GetUserById(int id);

    Task<User> CreateUser(User newUser);

    /// <summary>
    /// Update user using non-null properties of
    /// <see cref="Parameters.UpdateUser">UpdateUser</see>. Currently all the properties of user
    /// are non-null.
    /// </summary>
    Task<User> UpdateUser(int id, UpdateUser updatedUser);

    /// <summary>
    /// If old password is correct, change the password to new password and return true. Otherwise return false.
    /// </summary>
    Task<bool> ChangePassword(int id, string oldPassword, string newPassword);

    /// <summary>
    /// Check if the email exists in the database
    /// </summary>
    Task<bool> EmailExists(string email);

    Task DeductCredits(int userId, double credits);
}
