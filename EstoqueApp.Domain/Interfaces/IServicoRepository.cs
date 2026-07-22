namespace LidyDecorApp.Domain.Interfaces
{
    public interface IServicoRepository
    {
        Task<Servico?> GetServicoByIdAsync(int id);
        Task<IEnumerable<Servico>> GetServicosAsync();
        Task AddServicoAsync(Servico servico);
        Task UpdateServicoAsync(Servico servico);
        Task DeleteServicoAsync(int id);
    }
}
