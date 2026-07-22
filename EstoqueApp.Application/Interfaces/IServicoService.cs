using LidyDecorApp.Application.DTOs;

namespace LidyDecorApp.Application.Interfaces
{
    public interface IServicoService
    {
        Task<IEnumerable<ServicoDTO>> GetServicosAsync();
        Task<ServicoDTO?> GetServicoByIdAsync(int id);
        Task<ServicoDTO> AddServicoAsync(ServicoDTO dto);
        Task<ServicoDTO> UpdateServicoAsync(ServicoDTO dto);
        Task DeleteServicoAsync(int id);
    }
}
