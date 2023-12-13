using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Web.FormModels;

class RegisterModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(8, ErrorMessage = "Password must contain at least 8 characters")]
    public string Password { get; set; } = null!;
}