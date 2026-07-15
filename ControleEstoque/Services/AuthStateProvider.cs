using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private ClaimsPrincipal _userClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

    public AuthStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");
        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    await _localStorage.RemoveItemAsync("jwtToken");
                    _userClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
                }
                else
                {
                    var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
                    _userClaimsPrincipal = new ClaimsPrincipal(identity);
                }
            }
            catch
            {
                await _localStorage.RemoveItemAsync("jwtToken");
                _userClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            }
        }
        else
        {
            _userClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
        }

        return new AuthenticationState(_userClaimsPrincipal);
    }

    public void NotifyUserAuthentication(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
        _userClaimsPrincipal = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_userClaimsPrincipal)));
    }

    public void NotifyUserLogout()
    {
        _userClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_userClaimsPrincipal)));
    }
}
