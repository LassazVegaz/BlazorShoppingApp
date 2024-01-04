namespace TrendingApp.Packages.Authentication;

// These are contants exposed to the outside world. Some of them are reflections of internal constants.
public class Constants
{
    /// <summary>
    /// Name of the cookie used to store the JWT token
    /// </summary>
    public const string CookieName = InternalConstants.CookieName;
}
