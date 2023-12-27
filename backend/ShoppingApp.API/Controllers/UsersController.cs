using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.API.DTO.In;
using ShoppingApp.Core.Models;
using ShoppingApp.Core.Services;
using System.Text.RegularExpressions;

namespace ShoppingApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public partial class UsersController(IUsersService usersService, IMapper mapper) : ControllerBase
{
    private readonly IUsersService _usersService = usersService;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    public async Task<ActionResult> CreateUser(CreateUser newUser)
    {
        var newUserMapped = _mapper.Map<User>(newUser);
        await _usersService.CreateUser(newUserMapped);

        // when an empty Create result is returned, the status code is 204 (stupid .NET)
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet("emailExists/{email}")]
    public async Task<ActionResult<bool>> EmailExists(string email)
    {
        return await _usersService.EmailExists(email);
    }

    [GeneratedRegex(@"^(male|female|other)$", RegexOptions.IgnoreCase)]
    private static partial Regex GenderRegex();
}
