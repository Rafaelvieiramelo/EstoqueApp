using EstoqueApp.Application.DTOs;

namespace EstoqueApp.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> GetClientesAsync();
        Task<ClienteDTO> GetClienteByIdAsync(int id);
        Task<ClienteDTO> AddClienteAsync(ClienteDTO categoria);
        Task<ClienteDTO> UpdateClienteAsync(ClienteDTO categoria);
        Task DeleteClienteAsync(int id);
    }
}
