namespace EstoqueApp.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<Usuario?> GetUsuarioByEmailAsync(string email);
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task AddUsuarioAsync(Usuario usuario);
        Task UpdateUsuarioAsync(Usuario usuario);
        Task DeleteUsuarioAsync(int id);
    }
}
