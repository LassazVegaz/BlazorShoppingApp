namespace TrendingApp.Packages.Authentication.Services;

public interface ITokensService
{
    string GenerateToken(string userId);
}
