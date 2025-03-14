using LidyDecorApp.Application.DTOs;

namespace LidyDecorApp.Application.Interfaces
{
    public interface IUsuariosService
    {       
        Task<UsuarioReadDTO> GetUsuariosByEmailSenhaAsync(string email);
        Task<IEnumerable<UsuarioReadDTO>> GetUsuariosAsync();
        Task<UsuarioReadDTO> GetUsuariosByIdAsync(int id);
        Task<UsuarioWriteDTO> AddUsuariosAsync(UsuarioWriteDTO categoria);
        Task<UsuarioWriteDTO> UpdateUsuariosAsync(UsuarioWriteDTO categoria);
        Task DeleteUsuariosAsync(int id);
    }
}
