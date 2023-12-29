using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.API.DTO.In;

public class CreateUser
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }

    [EmailAddress]
    public string Email { get; set; } = default!;

    [MinLength(8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[a-zA-Z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one number, one uppercase letter, one lowercase letter and one special character from @$!%*?&")]
    public string Password { get; set; } = default!;
}
