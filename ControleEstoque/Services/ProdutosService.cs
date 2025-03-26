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

        return await _httpClient.GetFromJsonAsync<List<ProdutosModel>>("https://localhost:7071/Produtos");
    }

    public async Task<HttpResponseMessage> ExcluirProdutos(int idProdutos)
    {
        var token = await _localStorage.GetItemAsync<string>("jwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await _httpClient.DeleteAsync($"https://localhost:7071/Produtos/{idProdutos}");
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
            return await _httpClient.PatchAsJsonAsync("https://localhost:7071/Produtos", produtos);
        }
        else
        {
            return await _httpClient.PostAsJsonAsync("https://localhost:7071/Produtos", produtos);
        }
    }
}
