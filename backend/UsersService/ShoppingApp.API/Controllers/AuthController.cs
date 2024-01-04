using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersService.API.DTO.In;
using UsersService.API.DTO.Out;
using UsersService.Core.Services;
using AuthConstants = TrendingApp.Packages.Authentication.Constants;

namespace UsersService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService, IUsersService usersService, IMapper mapper) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IUsersService _usersService = usersService;
    private readonly IMapper _mapper = mapper;

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

    [HttpGet("profile")]
    [Authorize]
    public async Task<ActionResult> GetLoggedInUser()
    {
        var userId = User.Identity!.Name!;

        var user = await _usersService.GetUserById(int.Parse(userId));
        if (user is null) return Unauthorized();

        var mapped = _mapper.Map<UserDto>(user);

        return Ok(mapped);
    }
}
