using System.Net.Http.Json;
using LidyDecorApp.Web.Models;

public class ClientesService
{
    private readonly HttpClient _httpClient;

    public ClientesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ClientesModel>> GetClientesFromApi()
    {
        return await _httpClient.GetFromJsonAsync<List<ClientesModel>>("https://localhost:7071/Clientes");
    }

    public async Task<HttpResponseMessage> ExcluirClientes(int idClientes)
    {
        return await _httpClient.DeleteAsync($"https://localhost:7071/Clientes/{idClientes}");
    }

    public async Task<HttpResponseMessage> SalvarClientes(ClientesModel clientes, bool isEditMode)
    {
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
