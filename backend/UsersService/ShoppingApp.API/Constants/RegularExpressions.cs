namespace ShoppingApp.API.Constants;

public static class RegularExpressions
{
    public const string PasswordReg = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[a-zA-Z\d@$!%*?&]{8,}$";
}
