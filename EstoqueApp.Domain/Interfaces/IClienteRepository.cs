namespace EstoqueApp.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente> GetClienteByIdAsync(int id);
        Task<IEnumerable<Cliente>> GetClientesAsync();
        Task AddClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(Cliente cliente);
        Task DeleteClienteAsync(int id);
    }
}
