using EstoqueApp.Application.DTOs;

namespace EstoqueApp.Application.Interfaces
{
    public interface IOrcamentoService
    {
        Task<IEnumerable<OrcamentoDTO>> GetOrcamentosAsync();
        Task<IEnumerable<TipoEventoDTO>> GetTiposEventoAsync();
        Task<OrcamentoDTO> GetOrcamentoByIdAsync(int id);
        Task<OrcamentoDTO> AddOrcamentoAsync(OrcamentoDTO categoria);
        Task<OrcamentoDTO> UpdateOrcamentoAsync(OrcamentoDTO categoria);
        Task DeleteOrcamentoAsync(int id);
    }
}
