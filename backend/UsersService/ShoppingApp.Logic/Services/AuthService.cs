using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsersService.Core.Data;
using UsersService.Core.Options;
using UsersService.Core.Services;
using BC = BCrypt.Net.BCrypt;

namespace UsersService.Logic.Services;

public class AuthService(ShoppingAppContext context, IOptions<JwtOptions> jwtOptions) : IAuthService
{
    private readonly ShoppingAppContext _context = context;
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<string?> Login(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user is null) return null;

        var passwordValid = BC.Verify(password, user.Password);
        if (!passwordValid) return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([new Claim(ClaimTypes.Name, user.Id.ToString())]),
            Expires = DateTime.UtcNow.AddDays(_jwtOptions.ExpirationInDays),
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            SigningCredentials = signingCredentials
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
