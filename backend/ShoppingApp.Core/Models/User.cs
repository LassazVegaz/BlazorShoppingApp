using Microsoft.AspNetCore.Identity;

namespace ShoppingApp.Core.Models;

public class User : IdentityUser
{
    public DateOnly DateOfBirth { get; set; }
}
