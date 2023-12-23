using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.API.DTO.In;
using ShoppingApp.Core.Models;
using ShoppingApp.Core.Services;

namespace ShoppingApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUsersService usersService, IMapper mapper) : ControllerBase
{
    private readonly IUsersService _usersService = usersService;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    public async Task<ActionResult> CreateUser(CreateUser newUser)
    {
        try
        {
            var newUserMapped = _mapper.Map<User>(newUser);
            await _usersService.CreateUser(newUserMapped);

            // this endpoint does not provide the created user's details because
            // there is no endpoint get a user by id
            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("emailExists/{email}")]
    public async Task<ActionResult<bool>> EmailExists(string email)
    {
        try
        {
            return await _usersService.EmailExists(email);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
