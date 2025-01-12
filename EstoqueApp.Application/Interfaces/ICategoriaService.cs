using EstoqueApp.Application.DTOs;

namespace EstoqueApp.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaDTO>> GetCategoriasAsync();
        Task<CategoriaDTO> GetCategoriaByIdAsync(int id);
        Task<CategoriaDTO> AddCategoriaAsync(CategoriaDTO categoria);
        Task<CategoriaDTO> UpdateCategoriaAsync(CategoriaDTO categoria);
        Task DeleteCategoriaAsync(int id);
    }
}
