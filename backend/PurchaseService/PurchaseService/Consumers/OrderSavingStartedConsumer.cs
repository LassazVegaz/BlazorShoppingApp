using MassTransit;
using PurchaseService.Core;
using TrendingApp.Packages.Contracts.Sagas.Order;

namespace PurchaseService.Consumers;

public class OrderSavingStartedConsumer(IPurchaseManager purchaseManager, ILogger<OrderSavingStartedConsumer> logger)
    : IConsumer<OrderSavingStarted>
{
    private readonly IPurchaseManager _purchaseManager = purchaseManager;
    private readonly ILogger<OrderSavingStartedConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<OrderSavingStarted> context)
    {
        _logger.LogInformation("Creating purchase record");
        await _purchaseManager.Purchase(context.Message.UserId, context.Message.ItemId);
        var deduction = await _purchaseManager.GetItemPrice(context.Message.ItemId);

        await context.Publish(new OrderSavingFinished
        {
            CorrelationId = context.Message.CorrelationId,
            UserId = context.Message.UserId,
            Deduction = deduction,
        });
    }
}
