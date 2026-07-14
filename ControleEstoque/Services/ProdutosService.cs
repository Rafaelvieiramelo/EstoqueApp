using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using LidyDecorApp.Web.Models;

public class ProdutosService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public ProdutosService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<List<ProdutosModel>> GetProdutosFromApi()
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        try
        {
            var response = await _httpClient.GetAsync("Produtos");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<ProdutosModel>();
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ProdutosModel>>() ?? new List<ProdutosModel>();
        }
        catch
        {
            return new List<ProdutosModel>();
        }
    }

    public async Task<HttpResponseMessage> ExcluirProdutos(int idProdutos)
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await _httpClient.DeleteAsync($"Produtos/{idProdutos}");
    }

    public async Task<HttpResponseMessage> SalvarProdutos(ProdutosModel produtos, bool isEditMode)
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        if (isEditMode)
        {
            return await _httpClient.PatchAsJsonAsync("Produtos", produtos);
        }
        else
        {
            return await _httpClient.PostAsJsonAsync("Produtos", produtos);
        }
    }
}
