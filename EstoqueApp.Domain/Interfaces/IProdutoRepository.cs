namespace EstoqueApp.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> GetProdutoByIdAsync(int id);
        Task<IEnumerable<Produto>> GetProdutosAsync();
        Task AddProdutoAsync(Produto produto);
        Task UpdateProdutoAsync(Produto produto);
        Task DeleteProdutoAsync(int id);
    }
}
