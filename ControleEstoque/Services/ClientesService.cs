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

        try
        {
            var response = await _httpClient.GetAsync("Clientes");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<ClientesModel>();
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ClientesModel>>() ?? new List<ClientesModel>();
        }
        catch
        {
            return new List<ClientesModel>();
        }
    }

    public async Task<HttpResponseMessage> ExcluirClientes(int idClientes)
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await _httpClient.DeleteAsync($"Clientes/{idClientes}");
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
            return await _httpClient.PatchAsJsonAsync("Clientes", clientes);
        }
        else
        {
            return await _httpClient.PostAsJsonAsync("Clientes", clientes);
        }
    }
}
