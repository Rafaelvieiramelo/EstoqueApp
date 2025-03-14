namespace LidyDecorApp.Domain.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<Usuarios> GetUsuariosByIdAsync(int id);
        Task<Usuarios?> GetUsuariosByEmailAsync(string email);
        Task<IEnumerable<Usuarios>> GetUsuariossAsync();
        Task AddUsuariosAsync(Usuarios usuarios);
        Task UpdateUsuariosAsync(Usuarios usuarios);
        Task DeleteUsuariosAsync(int id);
    }
}
