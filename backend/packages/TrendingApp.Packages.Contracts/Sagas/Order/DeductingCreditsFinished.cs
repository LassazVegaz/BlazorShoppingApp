using MassTransit;

namespace TrendingApp.Packages.Contracts.Sagas.Order;

public record DeductingCreditsFinished : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
    public int UserId { get; set; }
}
