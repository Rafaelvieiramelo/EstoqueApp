using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using LidyDecorApp.Web.Models;

public class OrcamentosService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public OrcamentosService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<List<OrcamentosModel>> GetOrcamentosFromApi()
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await _httpClient.GetFromJsonAsync<List<OrcamentosModel>>("https://localhost:7071/Orcamentos");
    }

    public async Task<List<Tipoevento>> GetTiposEventoFromApi()
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await _httpClient.GetFromJsonAsync<List<Tipoevento>>("https://localhost:7071/Orcamentos/GetTiposEvento");
    }

    public async Task<HttpResponseMessage> ExcluirOrcamentos(int idOrcamentos)
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await _httpClient.DeleteAsync($"https://localhost:7071/Orcamentos/{idOrcamentos}");
    }

    public async Task<HttpResponseMessage> SalvarOrcamentos(OrcamentosModel orcamentos, bool isEditMode)
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        if (isEditMode)
        {
            return await _httpClient.PatchAsJsonAsync("https://localhost:7071/Orcamentos", orcamentos);
        }
        else
        {
            return await _httpClient.PostAsJsonAsync("https://localhost:7071/Orcamentos", orcamentos);
        }
    }
}
