using AutoMapper;
using MassTransit;
using PurchaseService.Models;
using TrendingApp.Packages.Contracts;

namespace PurchaseService.Consumers;

public class UserCreatedConsumer(PurchaseServiceContext context, IMapper mapper) : IConsumer<UserCreated>
{
    private readonly PurchaseServiceContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        var model = _mapper.Map<User>(context.Message);
        await _context.Users.AddAsync(model);
        await _context.SaveChangesAsync();
    }
}
