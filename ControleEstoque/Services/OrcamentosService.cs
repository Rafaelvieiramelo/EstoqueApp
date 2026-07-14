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

        try
        {
            var response = await _httpClient.GetAsync("Orcamentos");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<OrcamentosModel>();
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<OrcamentosModel>>() ?? new List<OrcamentosModel>();
        }
        catch
        {
            return new List<OrcamentosModel>();
        }
    }

    public async Task<List<Tipoevento>> GetTiposEventoFromApi()
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await _httpClient.GetFromJsonAsync<List<Tipoevento>>("Orcamentos/GetTiposEvento");
    }

    public async Task<HttpResponseMessage> ExcluirOrcamentos(int idOrcamentos)
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await _httpClient.DeleteAsync($"Orcamentos/{idOrcamentos}");
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
            return await _httpClient.PatchAsJsonAsync("Orcamentos", orcamentos);
        }
        else
        {
            return await _httpClient.PostAsJsonAsync("Orcamentos", orcamentos);
        }
    }

    public async Task<HttpResponseMessage> GetContratoFileStreamAsync(int orcamentoId)
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await _httpClient.GetAsync($"Orcamentos/{orcamentoId}/gerar-contrato");
    }

    public async Task<HttpResponseMessage> GetContratoPdfFileStreamAsync(int orcamentoId)
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await _httpClient.GetAsync($"Orcamentos/{orcamentoId}/gerar-contrato-pdf");
    }
}
