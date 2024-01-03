using System.ComponentModel.DataAnnotations;
using UsersService.API.Constants;

namespace UsersService.API.DTO.In;

public class ChangePassword
{
    public string OldPassword { get; set; } = default!;

    [RegularExpression(RegularExpressions.PasswordReg, ErrorMessage = "Password must be at least 8 characters long and contain at least one number, one uppercase letter, one lowercase letter and one special character from @$!%*?&")]
    public string NewPassword { get; set; } = default!;
}
