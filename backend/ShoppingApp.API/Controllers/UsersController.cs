using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Core.Services;

namespace ShoppingApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUsersService usersService) : ControllerBase
{
    private readonly IUsersService _usersService = usersService;

    [HttpGet("emailExists/{email}")]
    public async Task<ActionResult<bool>> EmailExists(string email) => await _usersService.EmailExists(email);
}
