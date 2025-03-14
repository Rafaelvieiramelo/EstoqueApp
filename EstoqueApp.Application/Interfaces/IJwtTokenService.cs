namespace LidyDecorApp.Application.Interfaces;
public interface IJwtTokenService
{
    string GenerateToken(string username, string role);
}
