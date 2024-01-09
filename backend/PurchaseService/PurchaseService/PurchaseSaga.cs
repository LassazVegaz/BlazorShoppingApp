using AutoMapper;
using MassTransit;
using TrendingApp.Packages.Contracts.Sagas.Order;

namespace PurchaseService;

public record PurchaseState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public State CurrentState { get; set; } = null!;
}

public class PurchaseSaga : MassTransitStateMachine<PurchaseState>
{
    private readonly IMapper _mapper;
    private readonly ILogger<PurchaseSaga> _logger;

    public State SavingOrder { get; set; } = null!;
    public State DeductingCredits { get; set; } = null!;
    public State RevertingSavedOrder { get; set; } = null!;

    public Event<UserPlacedOrder> UserPlacedOrder { get; set; } = null!;
    public Event<OrderSavingFinished> OrderSavingFinished { get; set; } = null!;
    public Event<DeductingCreditsFinished> DeductingCreditsFinished { get; set; } = null!;
    public Event<OrderSavingFailed> OrderSavingFailed { get; set; } = null!;
    public Event<DeductingCreditsFailed> DeductingCreditsFailed { get; set; } = null!;
    public Event<RevertingSavedOrderFinished> RevertingSavedOrderFinished { get; set; } = null!;

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
                .TransitionTo(DeductingCredits),
            When(OrderSavingFailed)
                .Then(context => _logger.LogInformation("OrderSavingFailed event consumed"))
                .PublishAsync(ctx => ctx.Init<RevertingSavedOrderStarted>(_mapper.Map<RevertingSavedOrderStarted>(ctx.Message)))
                .Then(context => _logger.LogInformation("RevertingSavedOrderStarted event published"))
                .TransitionTo(RevertingSavedOrder)
        );

        During(DeductingCredits,
            When(DeductingCreditsFinished)
                .Then(context => _logger.LogInformation("DeductingCreditsFinished event consumed"))
                .Finalize()
                .Then(context => _logger.LogInformation("Saga finished")),
            When(DeductingCreditsFailed)
                .Then(context => _logger.LogInformation("DeductingCreditsFailed event consumed"))
                .PublishAsync(ctx => ctx.Init<RevertingSavedOrderStarted>(_mapper.Map<RevertingSavedOrderStarted>(ctx.Message)))
                .Then(context => _logger.LogInformation("RevertingSavedOrderStarted event published"))
                .TransitionTo(RevertingSavedOrder)
        );

        During(RevertingSavedOrder,
            When(RevertingSavedOrderFinished)
                .Then(context => _logger.LogInformation("RevertingSavedOrderFinished event consumed"))
                .Finalize()
                .Then(context => _logger.LogInformation("Saga finished"))
        );
    }
}
