using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Core.DTO.In;
using ShoppingApp.Core.Services;

namespace ShoppingApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCredentials request)
    {
        var token = await _authService.Login(request.Email, request.Password);
        if (token is null) return Unauthorized("Invalid credentials");

        Response.Cookies.Append("jwt", token, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        return Ok();
    }
}
