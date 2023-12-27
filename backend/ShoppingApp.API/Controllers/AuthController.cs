using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Core.DTO.In;
using ShoppingApp.Core.Services;

namespace ShoppingApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService, IUsersService usersService) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IUsersService _usersService = usersService;

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

    [HttpGet("profile")]
    [Authorize]
    public async Task<ActionResult> GetLoggedInUser()
    {
        var userId = User.Identity!.Name!;

        var user = await _usersService.GetUserById(int.Parse(userId));
        if (user is null) return Unauthorized();

        return Ok(user);
    }
}
