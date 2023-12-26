using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.API.DTO.In;
using ShoppingApp.Core.Models;
using ShoppingApp.Logic.Proxies;

namespace ShoppingApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(UserManager userManager, IMapper mapper) : ControllerBase
{
    private readonly UserManager _userManager = userManager;
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
}
