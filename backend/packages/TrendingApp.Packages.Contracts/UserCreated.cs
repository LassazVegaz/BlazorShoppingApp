namespace TrendingApp.Packages.Contracts;

public record UserCreated
{
    public int Id { get; set; }
    public double Credits { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public DateOnly? EmailUpdatedOn { get; set; }
}
