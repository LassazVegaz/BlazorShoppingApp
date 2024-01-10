using AutoMapper;
using MassTransit;
using TrendingApp.Packages.Contracts;

namespace AuthService.Consumers;

public class UserCreatedConsumer(ILogger<UserCreatedConsumer> logger, AuthServiceContext context, IMapper mapper)
    : IConsumer<UserCreated>
{
    private readonly ILogger<UserCreatedConsumer> _logger = logger;
    private readonly AuthServiceContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        _logger.LogInformation("UserCreated event received");
        _logger.LogInformation("Creating user record: {id}", context.Message.Id);
        await _context.Users.AddAsync(_mapper.Map<User>(context.Message));
        await _context.SaveChangesAsync();
        _logger.LogInformation("Created user record: {id}", context.Message.Id);
    }
}
