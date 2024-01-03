namespace UsersService.API.DTO.Out;

/// <summary>
/// This class should be used whenever user data is returned to the client.
/// This has removed sensitive data like password.
/// </summary>
public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public DateOnly? EmailUpdatedOn { get; set; }
}
