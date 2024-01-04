using Microsoft.EntityFrameworkCore;
using TrendingApp.Packages.Authentication.Services;
using UsersService.Core.Data;
using UsersService.Core.Services;
using BC = BCrypt.Net.BCrypt;

namespace UsersService.Logic.Services;

public class AuthService(ShoppingAppContext context, ITokensService tokensService) : IAuthService
{
    private readonly ShoppingAppContext _context = context;
    private readonly ITokensService _tokensService = tokensService;

    public async Task<string?> Login(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user is null) return null;

        var passwordValid = BC.Verify(password, user.Password);
        if (!passwordValid) return null;

        return _tokensService.GenerateToken(user.Id.ToString());
    }
}
