using AutoMapper;
using PurchaseService.Models;
using TrendingApp.Packages.Contracts;

namespace PurchaseService;

public class Mapper : Profile
{
    public Mapper()
    {
        // source -> destination

        // contract -> model
        CreateMap<UserCreated, User>();
        CreateMap<ItemCreated, Item>();
    }
}
