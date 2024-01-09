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
        CreateMap<UserPlacedOrder, OrderSavingStarted>();
        CreateMap<OrderSavingStarted, OrderSavingFailed>();
        CreateMap<OrderSavingFinished, DeductingCreditsStarted>();
        CreateMap<DeductingCreditsStarted, RevertingSavedOrderFinished>();
        CreateMap<OrderSavingFailed, RevertingSavedOrderStarted>();
        CreateMap<RevertingSavedOrderStarted, RevertingSavedOrderFinished>();
    }
}
