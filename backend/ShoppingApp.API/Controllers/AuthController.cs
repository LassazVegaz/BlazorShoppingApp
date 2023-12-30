using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShoppingApp.API.Constants;
using ShoppingApp.API.DTO.Out;
using ShoppingApp.Core.DTO.In;
using ShoppingApp.Core.Options;
using ShoppingApp.Core.Services;

namespace ShoppingApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IOptions<JwtOptions> jwtOptions, IAuthService authService, IUsersService usersService, IMapper mapper) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IUsersService _usersService = usersService;
    private readonly IMapper _mapper = mapper;
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCredentials request, [FromQuery] bool useCookie = true)
    {
        var token = await _authService.Login(request.Email, request.Password);
        if (token is null) return Unauthorized("Invalid credentials");

        if (useCookie)
        {
            Response.Cookies.Append(CookieNames.JwtToken, token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = HttpContext.Request.IsHttps,
                Expires = DateTime.UtcNow.AddDays(_jwtOptions.ExpirationInDays)
            });
        }

        return Ok(token);
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        Response.Cookies.Delete(CookieNames.JwtToken);
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
