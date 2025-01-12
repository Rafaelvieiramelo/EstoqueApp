namespace EstoqueApp.Domain.Interfaces
{
    public interface IMovimentoEstoqueRepository
    {
        Task<MovimentoEstoque> GetMovimentoEstoqueByIdAsync(int id);
        Task<IEnumerable<MovimentoEstoque>> GetMovimentosEstoqueAsync();
        Task AddMovimentoEstoqueAsync(MovimentoEstoque movimentoEstoque);
        Task UpdateMovimentoEstoqueAsync(MovimentoEstoque movimentoEstoque);
        Task DeleteMovimentoEstoqueAsync(int id);
    }
}
