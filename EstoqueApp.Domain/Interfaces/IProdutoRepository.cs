namespace LidyDecorApp.Domain.Interfaces
{
    public interface IProdutosRepository
    {
        Task<Produtos> GetProdutosByIdAsync(int id);
        Task<IEnumerable<Produtos>> GetProdutossAsync();
        Task AddProdutosAsync(Produtos produtos);
        Task UpdateProdutosAsync(Produtos produtos);
        Task DeleteProdutosAsync(int id);
    }
}
