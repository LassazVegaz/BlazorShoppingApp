namespace ShoppingApp.Core.Services;

public interface IUsersService
{
    /// <summary>
    /// Check if the email exists in the database
    /// </summary>
    Task<bool> EmailExists(string email);
}
