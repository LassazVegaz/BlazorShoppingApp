using AutoMapper;
using MassTransit;
using PurchaseService.Core;
using TrendingApp.Packages.Contracts.Sagas.Order;

namespace PurchaseService.Consumers;

public class OrderSavingStartedConsumer(IPurchaseManager purchaseManager, ILogger<OrderSavingStartedConsumer> logger, IMapper mapper)
    : IConsumer<OrderSavingStarted>
{
    private readonly IPurchaseManager _purchaseManager = purchaseManager;
    private readonly ILogger<OrderSavingStartedConsumer> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task Consume(ConsumeContext<OrderSavingStarted> context)
    {
        _logger.LogInformation("OrderSavingStarted event received");
        _logger.LogInformation("Creating purchase record");
        try
        {
            await _purchaseManager.Purchase(context.Message.UserId, context.Message.ItemId);
            var deduction = await _purchaseManager.GetItemPrice(context.Message.ItemId);

            await context.Publish(new OrderSavingFinished
            {
                CorrelationId = context.Message.CorrelationId,
                UserId = context.Message.UserId,
                Deduction = deduction,
            });
            _logger.LogInformation("OrderSavingFinished event published");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed creating purchase record");
            await context.Publish(_mapper.Map<OrderSavingFailed>(context.Message));
        }
    }
}
