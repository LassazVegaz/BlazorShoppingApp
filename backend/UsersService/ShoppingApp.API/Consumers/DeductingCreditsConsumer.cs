using MassTransit;
using TrendingApp.Packages.Contracts.Sagas.Order;
using UsersService.Core.Services;

namespace UsersService.API.Consumers;

public class DeductingCreditsConsumer(ILogger<DeductingCreditsConsumer> logger, IUsersService usersService)
    : IConsumer<DeductingCreditsStarted>
{
    private readonly ILogger<DeductingCreditsConsumer> _logger = logger;
    private readonly IUsersService _usersService = usersService;

    public async Task Consume(ConsumeContext<DeductingCreditsStarted> context)
    {
        _logger.LogInformation("DeductingCreditsStarted event was received");
        await _usersService.DeductCredits(context.Message.UserId, context.Message.Deduction);
        _logger.LogInformation("Deducted user credits");

        await context.Publish(new DeductingCreditsFinished
        {
            CorrelationId = context.Message.CorrelationId,
            UserId = context.Message.UserId,
        });
    }
}
