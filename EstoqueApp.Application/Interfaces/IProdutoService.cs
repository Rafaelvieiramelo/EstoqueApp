using LidyDecorApp.Application.DTOs;

namespace LidyDecorApp.Application.Interfaces
{
    public interface IProdutosService
    {
        Task<IEnumerable<ProdutosDTO>> GetProdutosAsync();
        Task<ProdutosDTO> GetProdutosByIdAsync(int id);
        Task<ProdutosDTO> AddProdutosAsync(ProdutosDTO categoria);
        Task<ProdutosDTO> UpdateProdutosAsync(ProdutosDTO categoria);
        Task DeleteProdutosAsync(int id);
    }
}
