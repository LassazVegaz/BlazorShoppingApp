namespace ShoppingApp.API.DTO.In;

public class CreateUser
{
    public string? FirstName { get; set; } = "John";
    public string? LastName { get; set; } = "Doe";
    public string Email { get; set; } = null!;
    public string? Password { get; set; } = "a";
    public string? Gender { get; set; } = "male";
    public DateTime? DateOfBirth { get; set; } = new DateTime(2000, 1, 1);
}
