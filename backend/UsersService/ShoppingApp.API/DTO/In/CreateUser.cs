using System.ComponentModel.DataAnnotations;
using UsersService.API.Constants;

namespace UsersService.API.DTO.In;

public class CreateUser
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; }

    [EmailAddress]
    public string Email { get; set; } = default!;

    [MinLength(8)]
    [RegularExpression(RegularExpressions.PasswordReg, ErrorMessage = "Password must be at least 8 characters long and contain at least one number, one uppercase letter, one lowercase letter and one special character from @$!%*?&")]
    public string Password { get; set; } = default!;
}
