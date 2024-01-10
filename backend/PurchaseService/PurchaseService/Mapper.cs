using AutoMapper;
using PurchaseService.Models;
using TrendingApp.Packages.Contracts;
using TrendingApp.Packages.Contracts.Sagas.Order;

namespace PurchaseService;

public class Mapper : Profile
{
    public Mapper()
    {
        // source -> destination

        // contract -> model
        CreateMap<UserCreated, User>();
        CreateMap<ItemCreated, Item>();

        // event -> event
        CreateMap<OrderSavingStarted, OrderSavingFailed>();
        CreateMap<RevertingSavedOrderStarted, RevertingSavedOrderFinished>();

        // state -> event
        CreateMap<PurchaseState, OrderSavingStarted>();
        CreateMap<PurchaseState, DeductingCreditsStarted>();
        CreateMap<PurchaseState, RevertingSavedOrderStarted>();
    }
}
