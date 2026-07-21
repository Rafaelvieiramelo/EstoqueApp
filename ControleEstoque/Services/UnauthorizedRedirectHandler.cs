using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

public class UnauthorizedRedirectHandler : DelegatingHandler
{
    private readonly NavigationManager _navigationManager;
    private readonly AuthStateProvider _authStateProvider;
    private readonly Blazored.LocalStorage.ILocalStorageService _localStorage;

    public UnauthorizedRedirectHandler(
        NavigationManager navigationManager, 
        AuthStateProvider authStateProvider,
        Blazored.LocalStorage.ILocalStorageService localStorage)
    {
        _navigationManager = navigationManager;
        _authStateProvider = authStateProvider;
        _localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            // Limpa o token expirado/inválido do localStorage
            await _localStorage.RemoveItemAsync("jwtToken");
            
            // Notifica o Blazor de que o usuário foi deslogado
            _authStateProvider.NotifyUserLogout();
            
            // Redireciona para a tela de login
            _navigationManager.NavigateTo("/login");
        }

        return response;
    }
}
