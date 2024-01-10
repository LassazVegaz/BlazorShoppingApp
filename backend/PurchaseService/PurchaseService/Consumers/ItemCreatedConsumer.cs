using AutoMapper;
using MassTransit;
using PurchaseService.Models;
using TrendingApp.Packages.Contracts;

namespace PurchaseService.Consumers;

public class ItemCreatedConsumer(PurchaseServiceContext context, IMapper mapper, ILogger<ItemCreatedConsumer> logger)
    : IConsumer<ItemCreated>
{
    private readonly PurchaseServiceContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ItemCreatedConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<ItemCreated> context)
    {
        _logger.LogInformation("Item created event received: {id}", context.Message.Id);

        var model = _mapper.Map<Item>(context.Message);
        await _context.Items.AddAsync(model);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Item saved to database: {id}", context.Message.Id);
    }
}
