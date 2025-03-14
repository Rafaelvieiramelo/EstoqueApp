using LidyDecorApp.Application.DTOs;

namespace LidyDecorApp.Application.Interfaces
{
    public interface IClientesService
    {
        Task<IEnumerable<ClientesDTO>> GetClientesAsync();
        Task<ClientesDTO> GetClientesByIdAsync(int id);
        Task<ClientesDTO> AddClientesAsync(ClientesDTO categoria);
        Task<ClientesDTO> UpdateClientesAsync(ClientesDTO categoria);
        Task DeleteClientesAsync(int id);
    }
}
