using AutoMapper;
using EstoqueApp.Application.DTOs;
using EstoqueApp.Application.Exceptions;
using EstoqueApp.Application.Interfaces;
using EstoqueApp.Domain;
using EstoqueApp.Domain.Interfaces;

namespace EstoqueApp.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioDTO> GetUsuarioByEmailSenhaAsync(string email)
        {
            var usuario = await _usuarioRepository.GetUsuarioByEmailAsync(email);
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<IEnumerable<UsuarioDTO>> GetUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.GetUsuariosAsync();
            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);

            return usuariosDto;
        }

        public async Task<UsuarioDTO> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<UsuarioDTO> AddUsuarioAsync(UsuarioDTO UsuarioDTO)
        {
            try
            {
                var usuario = _mapper.Map<Usuario>(UsuarioDTO);
                await _usuarioRepository.AddUsuarioAsync(usuario);

                return _mapper.Map<UsuarioDTO>(usuario);
            }
            catch (Exception)
            {
                return new UsuarioDTO();
            }
        }

        public async Task<UsuarioDTO> UpdateUsuarioAsync(UsuarioDTO usuario)
        {
            try
            {
                await _usuarioRepository.UpdateUsuarioAsync(_mapper.Map<Usuario>(usuario));
                return usuario;
            }
            catch (Exception)
            {
                return new UsuarioDTO();
            }            
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            try
            {
                await _usuarioRepository.DeleteUsuarioAsync(id);
            }
            catch (Exception ex)
            {
                throw new DeleteException(ex.Message);
            }
        }
    }
}
