using AutoMapper;
using TrendingApp.Packages.Contracts.Sagas.Order;
using UsersService.API.DTO.In;
using UsersService.API.DTO.Out;
using UsersService.Core.Models;
using UsersService.Core.Options;

namespace UsersService.API.Mapper;

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

        // event -> event
        CreateMap<DeductingCreditsStarted, DeductingCreditsFinished>();
        CreateMap<DeductingCreditsStarted, DeductingCreditsFailed>();
    }
}
