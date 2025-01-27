namespace EstoqueApp.Application.Interfaces;
public interface IJwtTokenService
{
    string GenerateToken(string username, string role);
}
