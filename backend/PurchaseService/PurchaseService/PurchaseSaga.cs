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

    public State SavingOrder { get; set; } = null!;
    public State DeductingCredits { get; set; } = null!;

    public Event<UserPlacedOrder> UserPlacedOrder { get; set; } = null!;
    public Event<OrderSavingFinished> OrderSavingFinished { get; set; } = null!;
    public Event<DeductingCreditsFinished> DeductingCreditsFinished { get; set; } = null!;

    public PurchaseSaga(IMapper mapper)
    {
        _mapper = mapper;

        Initially(
            When(UserPlacedOrder)
                .PublishAsync(ctx => ctx.Init<OrderSavingStarted>(_mapper.Map<OrderSavingStarted>(ctx.Message)))
                .TransitionTo(SavingOrder)
        );

        During(SavingOrder,
            When(OrderSavingFinished)
                .PublishAsync(ctx => ctx.Init<DeductingCreditsStarted>(_mapper.Map<DeductingCreditsStarted>(ctx.Message)))
                .TransitionTo(DeductingCredits)
        );

        During(DeductingCredits,
            When(DeductingCreditsFinished)
                .Finalize()
        );
    }
}
