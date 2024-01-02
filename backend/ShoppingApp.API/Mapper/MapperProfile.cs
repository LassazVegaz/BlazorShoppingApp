using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ShoppingApp.API.DTO.In;
using ShoppingApp.API.DTO.Out;
using ShoppingApp.Core.Models;

namespace ShoppingApp.API.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // source first, destination second

        // sending to client
        CreateMap<User, UserDto>();
        CreateMap<UserOptions, UserOptionsDto>();

        // receiving from client
        CreateMap<CreateUser, User>();
    }
}
