// Camada de Aplicação (Application)
using EstoqueApp.Application.DTOs;
using System.Net.Http.Json;

namespace EstoqueApp.Application.Services
{
    public class WebProdutoService
    {
        private readonly HttpClient _httpClient;

        public WebProdutoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProdutoDTO>> GetProdutosAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ProdutoDTO>>("https://localhost:7071/Produtos/GetProdutos");
                return response ?? new List<ProdutoDTO>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro ao chamar a API: {ex.Message}");
                return new List<ProdutoDTO>(); // Retorna uma lista vazia em caso de erro
            }
        }
    }
}
