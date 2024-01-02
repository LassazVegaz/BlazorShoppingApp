using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShoppingApp.API.DTO.In;
using ShoppingApp.API.DTO.Out;
using ShoppingApp.Core.Models;
using ShoppingApp.Core.Options;
using ShoppingApp.Core.Parameters;
using ShoppingApp.Core.Services;
using System.Text.RegularExpressions;

namespace ShoppingApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public partial class UsersController(IOptions<UserOptions> userOptions, IUsersService usersService, IMapper mapper) : ControllerBase
{
    private readonly UserOptions _userOptions = userOptions.Value;
    private readonly IUsersService _usersService = usersService;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] CreateUser newUser)
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

    [HttpPatch]
    [Authorize]
    public async Task<ActionResult<UserDto>> UpdateUser([FromBody] UpdateUser data)
    {
        var id = int.Parse(User.Identity!.Name!);
        var user = await _usersService.UpdateUser(id, data);
        var userDto = _mapper.Map<UserDto>(user);

        return userDto;
    }

    [HttpGet("options")]
    public ActionResult<UserOptionsDto> GetUserOptions() => _mapper.Map<UserOptionsDto>(_userOptions);

    [GeneratedRegex(@"^(male|female|other)$", RegexOptions.IgnoreCase)]
    private static partial Regex GenderRegex();
}
