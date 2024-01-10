using MassTransit;

namespace TrendingApp.Packages.Contracts.Sagas.Order;

public record DeductingCreditsStarted : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
    public int UserId { get; set; }
    public double Deduction { get; set; }
}
