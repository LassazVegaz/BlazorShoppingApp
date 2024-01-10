using AutoMapper;
using MassTransit;
using TrendingApp.Packages.Contracts.Sagas.Order;
using UsersService.API.Core;

namespace UsersService.API.Consumers;

public class DeductingCreditsConsumer(ILogger<DeductingCreditsConsumer> logger, IUsersService usersService, IMapper mapper)
    : IConsumer<DeductingCreditsStarted>
{
    private readonly ILogger<DeductingCreditsConsumer> _logger = logger;
    private readonly IUsersService _usersService = usersService;
    private readonly IMapper _mapper = mapper;

    public async Task Consume(ConsumeContext<DeductingCreditsStarted> context)
    {
        _logger.LogInformation("DeductingCreditsStarted event was received");
        try
        {
            await _usersService.DeductCredits(context.Message.UserId, context.Message.Deduction);
            _logger.LogInformation("Deducted user credits");

            await context.Publish(_mapper.Map<DeductingCreditsFinished>(context.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Deducting credits failed");
            await context.Publish(_mapper.Map<DeductingCreditsFailed>(context.Message));
        }

    }
}
