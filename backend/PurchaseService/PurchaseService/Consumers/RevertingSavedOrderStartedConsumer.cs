using AutoMapper;
using MassTransit;
using PurchaseService.Core;
using TrendingApp.Packages.Contracts.Sagas.Order;

namespace PurchaseService.Consumers;

public class RevertingSavedOrderStartedConsumer(IPurchaseManager purchaseManager, ILogger<RevertingSavedOrderStartedConsumer> logger, IMapper mapper)
    : IConsumer<RevertingSavedOrderStarted>
{
    private readonly IPurchaseManager _purchaseManager = purchaseManager;
    private readonly ILogger<RevertingSavedOrderStartedConsumer> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task Consume(ConsumeContext<RevertingSavedOrderStarted> context)
    {
        _logger.LogInformation("RevertingSavedOrderStarted event received");
        _logger.LogInformation("Checking if purchase record exists");
        if (await _purchaseManager.IsPurchased(context.Message.UserId, context.Message.ItemId))
        {
            _logger.LogInformation("Removing purchase record");
            await _purchaseManager.RevertPurchase(context.Message.UserId, context.Message.ItemId);
        }
        else
        {
            _logger.LogInformation("Purchase record not found");
        }

        await context.Publish(_mapper.Map<RevertingSavedOrderFinished>(context.Message));
    }
}
