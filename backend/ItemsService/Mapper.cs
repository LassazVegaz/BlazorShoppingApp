using AutoMapper;
using ItemsService.DTO.Out;
using ItemsService.Models;

namespace ItemsService;

public class Mapper : Profile
{
    public Mapper()
    {
        // source -> destination

        // DTO -> Model
        CreateMap<CreateItem, Item>();
    }
}
