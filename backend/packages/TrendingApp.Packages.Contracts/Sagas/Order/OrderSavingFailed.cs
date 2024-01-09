using MassTransit;

namespace TrendingApp.Packages.Contracts.Sagas.Order;

public record OrderSavingFailed : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
    public int UserId { get; set; }
    public int ItemId { get; set; }
}
