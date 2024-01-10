using AuthService.Core;
using Microsoft.EntityFrameworkCore;
using TrendingApp.Packages.Authentication.Services;
using BC = BCrypt.Net.BCrypt;

namespace AuthService;

public class AuthManager(AuthServiceContext context, ITokensService tokensService) : IAuthManager
{
    private readonly AuthServiceContext _context = context;
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
