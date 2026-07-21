using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using LidyDecorApp.Web.Models;

public class ServicosService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public ServicosService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<List<ServicosModel>> GetServicosFromApi()
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        try
        {
            var response = await _httpClient.GetAsync("Servicos");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<ServicosModel>();
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ServicosModel>>() ?? new List<ServicosModel>();
        }
        catch
        {
            return new List<ServicosModel>();
        }
    }

    public async Task<HttpResponseMessage> ExcluirServicos(int idServico)
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await _httpClient.DeleteAsync($"Servicos/{idServico}");
    }

    public async Task<HttpResponseMessage> SalvarServicos(ServicosModel servico, bool isEditMode)
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        if (isEditMode)
        {
            return await _httpClient.PutAsJsonAsync("Servicos", servico);
        }
        else
        {
            return await _httpClient.PostAsJsonAsync("Servicos", servico);
        }
    }
}
