using Microsoft.AspNetCore.Identity;

namespace ShoppingApp.Core.Models;

public class User : IdentityUser
{
    public string Gender { get; set; } = null!;
}
