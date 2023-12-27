using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.API.DTO.In;
using ShoppingApp.Core.Models;
using ShoppingApp.Logic.Proxies;

namespace ShoppingApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(UserManager userManager, SignInManager signInManager, IMapper mapper) : ControllerBase
{
    private readonly UserManager _userManager = userManager;
    private readonly SignInManager _signInManager = signInManager;
    private readonly IMapper _mapper = mapper;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUser createUser)
    {
        var user = _mapper.Map<User>(createUser);
        var result = await _userManager.CreateAsync(user, createUser.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginCredentials credentials)
    {
        var user = await _userManager.FindByEmailAsync(credentials.Email);

        if (user == null) return Unauthorized();

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, credentials.Password);

        if (!isPasswordValid) return Unauthorized();

        var token = await _signInManager.GenerateTokenAsync(user);

        return Ok(token);
    }
}
