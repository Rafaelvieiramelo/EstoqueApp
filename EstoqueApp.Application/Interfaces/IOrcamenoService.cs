using LidyDecorApp.Application.DTOs;

namespace LidyDecorApp.Application.Interfaces
{
    public interface IOrcamentosService
    {
        Task<IEnumerable<OrcamentosDTO>> GetOrcamentosAsync();
        Task<string> GetNumeroUltimoOrcamentosAsync();
        Task<IEnumerable<TipoEventoDTO>> GetTiposEventoAsync();
        Task<OrcamentosDTO> GetOrcamentosByIdAsync(int id);
        Task<OrcamentosDTO> AddOrcamentosAsync(OrcamentosDTO categoria);
        Task<OrcamentosDTO> UpdateOrcamentosAsync(OrcamentosDTO categoria);
        Task DeleteOrcamentosAsync(int id);
    }
}
