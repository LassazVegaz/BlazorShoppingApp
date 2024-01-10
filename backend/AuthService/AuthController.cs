using AuthService.Core;
using AuthService.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthConstants = TrendingApp.Packages.Authentication.Constants;

namespace AuthService;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthManager authManager) : ControllerBase
{
    private readonly IAuthManager _authService = authManager;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCredentials request, [FromQuery] bool useCookie = true)
    {
        var token = await _authService.Login(request.Email, request.Password);
        if (token is null) return Unauthorized("Invalid credentials");

        if (useCookie)
        {
            Response.Cookies.Append(AuthConstants.CookieName, token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = HttpContext.Request.IsHttps,
                Expires = DateTime.UtcNow.AddDays(AuthConstants.TokenExpirationDays)
            });
        }

        return Ok(token);
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        Response.Cookies.Delete(AuthConstants.CookieName);
        return Ok();
    }
}
