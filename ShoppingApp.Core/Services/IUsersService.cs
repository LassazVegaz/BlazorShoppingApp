using ShoppingApp.Core.Models;

namespace ShoppingApp.Core.Services;

public interface IUsersService
{
    User CreateUser(User newUser);
}
