using AutoMapper;
using ShoppingApp.Core.Models;
using ShoppingApp.Web.FormModels;

namespace ShoppingApp.Web.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<RegisterModel, User>();
    }
}
