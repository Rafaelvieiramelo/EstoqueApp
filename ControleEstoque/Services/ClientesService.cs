using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using LidyDecorApp.Web.Models;

public class ClientesService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public ClientesService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<List<ClientesModel>> GetClientesFromApi()
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await _httpClient.GetFromJsonAsync<List<ClientesModel>>("https://localhost:7071/Clientes");
    }

    public async Task<HttpResponseMessage> ExcluirClientes(int idClientes)
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await _httpClient.DeleteAsync($"https://localhost:7071/Clientes/{idClientes}");
    }

    public async Task<HttpResponseMessage> SalvarClientes(ClientesModel clientes, bool isEditMode)
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        if (isEditMode)
        {
            return await _httpClient.PatchAsJsonAsync("https://localhost:7071/Clientes", clientes);
        }
        else
        {
            return await _httpClient.PostAsJsonAsync("https://localhost:7071/Clientes", clientes);
        }
    }
}
