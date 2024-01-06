namespace TrendingApp.Packages.Contracts;

public record ItemCreated
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Price { get; set; }
}
