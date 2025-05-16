using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using LidyDecorApp.Web.Models;

public class UsuariosService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public UsuariosService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<List<UsuariosModel>> GetUsuariosFromApi()
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await _httpClient.GetFromJsonAsync<List<UsuariosModel>>("https://localhost:7071/Usuarios");
    }

    public async Task<HttpResponseMessage> ExcluirUsuarios(int idUsuarios)
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await _httpClient.DeleteAsync($"https://localhost:7071/Usuarios/{idUsuarios}");
    }

    public async Task<HttpResponseMessage> SalvarUsuarios(UsuariosModel usuarios, bool isEditMode)
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        if (isEditMode)
        {
            return await _httpClient.PatchAsJsonAsync("https://localhost:7071/Usuarios", usuarios);
        }
        else
        {
            return await _httpClient.PostAsJsonAsync("https://localhost:7071/Usuarios", usuarios);
        }
    }
}
