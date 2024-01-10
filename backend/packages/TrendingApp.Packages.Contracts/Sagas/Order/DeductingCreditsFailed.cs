using MassTransit;

namespace TrendingApp.Packages.Contracts.Sagas.Order;

public record DeductingCreditsFailed : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
    public int UserId { get; set; }
}
