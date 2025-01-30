using EstoqueApp.Application.DTOs;

namespace EstoqueApp.Application.Interfaces
{
    public interface IUsuarioService
    {       
        Task<UsuarioDTO> GetUsuarioByEmailSenhaAsync(string email);
        Task<IEnumerable<UsuarioDTO>> GetUsuariosAsync();
        Task<UsuarioDTO> GetUsuarioByIdAsync(int id);
        Task<UsuarioDTO> AddUsuarioAsync(UsuarioDTO categoria);
        Task<UsuarioDTO> UpdateUsuarioAsync(UsuarioDTO categoria);
        Task DeleteUsuarioAsync(int id);
    }
}
