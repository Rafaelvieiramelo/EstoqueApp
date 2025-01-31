namespace EstoqueApp.Domain.Interfaces
{
    public interface IOrcamentoRepository
    {
        Task<Orcamento> GetOrcamentoByIdAsync(int id);
        Task<IEnumerable<Orcamento>> GetOrcamentosAsync();
        Task<IEnumerable<TipoEvento>> GetTiposEventoAsync();
        Task AddOrcamentoAsync(Orcamento orcamento);
        Task UpdateOrcamentoAsync(Orcamento orcamento);
        Task DeleteOrcamentoAsync(int id);
    }
}
