// Camada de Aplicação (Application)
using LidyDecorApp.Application.DTOs;
using System.Net.Http.Json;

namespace LidyDecorApp.Application.Services
{
    public class WebProdutosService
    {
        private readonly HttpClient _httpClient;

        public WebProdutosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProdutosDTO>> GetProdutossAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ProdutosDTO>>("https://localhost:7071/Produtoss/GetProdutoss");
                return response ?? new List<ProdutosDTO>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro ao chamar a API: {ex.Message}");
                return new List<ProdutosDTO>(); // Retorna uma lista vazia em caso de erro
            }
        }
    }
}
