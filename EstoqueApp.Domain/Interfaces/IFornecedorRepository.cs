namespace EstoqueApp.Domain.Interfaces
{
    public interface IFornecedorRepository
    {
        Task<Fornecedor> GetFornecedorByIdAsync(int id);
        Task<IEnumerable<Fornecedor>> GetFornecedoresAsync();
        Task AddFornecedorAsync(Fornecedor fornecedor);
        Task UpdateFornecedorAsync(Fornecedor fornecedor);
        Task DeleteFornecedorAsync(int id);
    }

}
