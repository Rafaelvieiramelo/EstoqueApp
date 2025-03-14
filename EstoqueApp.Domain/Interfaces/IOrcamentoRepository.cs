namespace LidyDecorApp.Domain.Interfaces
{
    public interface IOrcamentosRepository
    {
        Task<Orcamentos> GetOrcamentosByIdAsync(int id);
        Task<string?> GetNumeroUltimoOrcamentosAsync();
        Task<IEnumerable<Orcamentos>> GetOrcamentossAsync();
        Task<IEnumerable<TipoEventos>> GetTiposEventoAsync();
        Task AddOrcamentosAsync(Orcamentos orcamentos);
        Task UpdateOrcamentosAsync(Orcamentos orcamentos);
        Task DeleteOrcamentosAsync(int id);
    }
}
