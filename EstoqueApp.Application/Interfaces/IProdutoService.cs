using EstoqueApp.Application.DTOs;

namespace EstoqueApp.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDTO>> GetProdutosAsync();
        Task<ProdutoDTO> GetProdutoByIdAsync(int id);
        Task<ProdutoDTO> AddProdutoAsync(ProdutoDTO categoria);
        Task<ProdutoDTO> UpdateProdutoAsync(ProdutoDTO categoria);
        Task DeleteProdutoAsync(int id);
    }
}
