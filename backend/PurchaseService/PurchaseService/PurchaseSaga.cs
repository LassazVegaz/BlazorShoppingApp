using AutoMapper;
using MassTransit;
using TrendingApp.Packages.Contracts.Sagas.Order;

namespace PurchaseService;

internal record PurchaseState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public State CurrentState { get; set; } = null!;
}

internal class PurchaseSaga : MassTransitStateMachine<PurchaseState>
{
    private readonly IMapper _mapper;
    private readonly ILogger<PurchaseSaga> _logger;

    public State SavingOrder { get; set; } = null!;
    public State DeductingCredits { get; set; } = null!;

    public Event<UserPlacedOrder> UserPlacedOrder { get; set; } = null!;
    public Event<OrderSavingFinished> OrderSavingFinished { get; set; } = null!;
    public Event<DeductingCreditsFinished> DeductingCreditsFinished { get; set; } = null!;

    public PurchaseSaga(IMapper mapper, ILogger<PurchaseSaga> logger)
    {
        _mapper = mapper;
        _logger = logger;

        Initially(
            When(UserPlacedOrder)
                .Then(context => _logger.LogInformation("UserPlacedOrder event consumed"))
                .PublishAsync(ctx => ctx.Init<OrderSavingStarted>(_mapper.Map<OrderSavingStarted>(ctx.Message)))
                .Then(context => _logger.LogInformation("OrderSavingStarted event published"))
                .TransitionTo(SavingOrder)
        );

        During(SavingOrder,
            When(OrderSavingFinished)
                .Then(context => _logger.LogInformation("OrderSavingFinished event consumed"))
                .PublishAsync(ctx => ctx.Init<DeductingCreditsStarted>(_mapper.Map<DeductingCreditsStarted>(ctx.Message)))
                .Then(context => _logger.LogInformation("DeductingCreditsStarted event published"))
                .TransitionTo(DeductingCredits)
        );

        During(DeductingCredits,
            When(DeductingCreditsFinished)
                .Then(context => _logger.LogInformation("DeductingCreditsFinished event consumed"))
                .Finalize()
                .Then(context => _logger.LogInformation("Saga finished"))
        );
    }
}
