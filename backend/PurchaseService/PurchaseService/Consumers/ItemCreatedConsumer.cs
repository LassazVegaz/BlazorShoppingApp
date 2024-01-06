using AutoMapper;
using MassTransit;
using PurchaseService.Models;
using TrendingApp.Packages.Contracts;

namespace PurchaseService.Consumers;

public class ItemCreatedConsumer(PurchaseServiceContext context, IMapper mapper) : IConsumer<ItemCreated>
{
    private readonly PurchaseServiceContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task Consume(ConsumeContext<ItemCreated> context)
    {
        var model = _mapper.Map<Item>(context.Message);
        await _context.Items.AddAsync(model);
        await _context.SaveChangesAsync();
    }
}
