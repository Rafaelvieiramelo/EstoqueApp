namespace EstoqueApp.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<Categoria> GetCategoriaByIdAsync(int id);
        Task<IEnumerable<Categoria>> GetCategoriasAsync();
        Task AddCategoriaAsync(Categoria categoria);
        Task UpdateCategoriaAsync(Categoria categoria);
        Task DeleteCategoriaAsync(int id);
    }
}
