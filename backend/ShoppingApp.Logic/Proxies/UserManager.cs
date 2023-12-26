using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShoppingApp.Core.Models;

namespace ShoppingApp.Logic.Proxies;

public class UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger)
    : UserManager<User>(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
{
    public override Task<IdentityResult> CreateAsync(User user, string password)
    {
        // username is email
        // currently logic of the application does not include a unique 'user name' concept (like in Insta)
        user.UserName = user.Email;
        return base.CreateAsync(user, password);
    }
}
