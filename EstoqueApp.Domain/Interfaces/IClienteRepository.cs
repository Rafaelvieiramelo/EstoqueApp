namespace LidyDecorApp.Domain.Interfaces
{
    public interface IClientesRepository
    {
        Task<Clientes> GetClientesByIdAsync(int id);
        Task<IEnumerable<Clientes>> GetClientessAsync();
        Task AddClientesAsync(Clientes clientes);
        Task UpdateClientesAsync(Clientes clientes);
        Task DeleteClientesAsync(int id);
    }
}
