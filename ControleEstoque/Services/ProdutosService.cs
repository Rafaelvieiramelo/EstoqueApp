using System.Net.Http.Json;
using LidyDecorApp.Web.Models;

public class ProdutosService
{
    private readonly HttpClient _httpClient;

    public ProdutosService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProdutosModel>> GetProdutosFromApi()
    {
        return await _httpClient.GetFromJsonAsync<List<ProdutosModel>>("https://localhost:7071/Produtos");
    }

    public async Task<HttpResponseMessage> ExcluirProdutos(int idProdutos)
    {
        return await _httpClient.DeleteAsync($"https://localhost:7071/Produtos/{idProdutos}");
    }

    public async Task<HttpResponseMessage> SalvarProdutos(ProdutosModel produtos, bool isEditMode)
    {
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
