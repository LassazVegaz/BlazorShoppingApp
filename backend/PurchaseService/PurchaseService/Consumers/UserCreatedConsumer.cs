using AutoMapper;
using MassTransit;
using PurchaseService.Models;
using TrendingApp.Packages.Contracts;

namespace PurchaseService.Consumers;

public class UserCreatedConsumer(PurchaseServiceContext context, IMapper mapper, ILogger<UserCreatedConsumer> logger)
    : IConsumer<UserCreated>
{
    private readonly PurchaseServiceContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<UserCreatedConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        _logger.LogInformation("User created even received: {id}", context.Message.Id);

        var model = _mapper.Map<User>(context.Message);
        await _context.Users.AddAsync(model);
        await _context.SaveChangesAsync();

        _logger.LogInformation("User saved to database: {id}", context.Message.Id);
    }
}
