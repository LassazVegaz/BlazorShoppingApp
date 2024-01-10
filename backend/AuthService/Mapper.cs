using AutoMapper;
using TrendingApp.Packages.Contracts;

namespace AuthService;

public class Mapper : Profile
{
    public Mapper()
    {
        // source -> destination

        // event -> model
        CreateMap<UserCreated, User>();
    }
}
