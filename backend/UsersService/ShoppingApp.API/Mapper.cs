using AutoMapper;
using TrendingApp.Packages.Contracts.Sagas.Order;
using UsersService.DTO.In;
using UsersService.DTO.Out;
using UsersService.Options;

namespace UsersService;

public class Mapper : Profile
{
    public Mapper()
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
