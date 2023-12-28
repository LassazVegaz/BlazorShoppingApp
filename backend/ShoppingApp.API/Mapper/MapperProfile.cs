using AutoMapper;
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

        // receiving from client
        CreateMap<CreateUser, User>();
    }
}
