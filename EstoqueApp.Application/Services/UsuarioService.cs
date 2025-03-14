using AutoMapper;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Exceptions;
using LidyDecorApp.Application.Interfaces;
using LidyDecorApp.Domain;
using LidyDecorApp.Domain.Interfaces;

namespace LidyDecorApp.Application.Services
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IMapper _mapper;

        public UsuariosService(IUsuariosRepository usuariosRepository, IMapper mapper)
        {
            _usuariosRepository = usuariosRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioReadDTO> GetUsuariosByEmailSenhaAsync(string email)
        {
            var usuarios = await _usuariosRepository.GetUsuariosByEmailAsync(email);
            return _mapper.Map<UsuarioReadDTO>(usuarios);
        }

        public async Task<IEnumerable<UsuarioReadDTO>> GetUsuariosAsync()
        {
            var usuarioss = await _usuariosRepository.GetUsuariossAsync();
            var usuariossDto = _mapper.Map<IEnumerable<UsuarioReadDTO>>(usuarioss);

            return usuariossDto;
        }

        public async Task<UsuarioReadDTO> GetUsuariosByIdAsync(int id)
        {
            var usuarios = await _usuariosRepository.GetUsuariosByIdAsync(id);
            return _mapper.Map<UsuarioReadDTO>(usuarios);
        }

        public async Task<UsuarioWriteDTO> AddUsuariosAsync(UsuarioWriteDTO UsuariosDTO)
        {
            try
            {
                var usuarios = _mapper.Map<Usuarios>(UsuariosDTO);
                await _usuariosRepository.AddUsuariosAsync(usuarios);

                return _mapper.Map<UsuarioWriteDTO>(usuarios);
            }
            catch (Exception)
            {
                return new UsuarioWriteDTO();
            }
        }

        public async Task<UsuarioWriteDTO> UpdateUsuariosAsync(UsuarioWriteDTO usuarios)
        {
            try
            {
                await _usuariosRepository.UpdateUsuariosAsync(_mapper.Map<Usuarios>(usuarios));
                return usuarios;
            }
            catch (Exception)
            {
                return new UsuarioWriteDTO();
            }            
        }

        public async Task DeleteUsuariosAsync(int id)
        {
            try
            {
                await _usuariosRepository.DeleteUsuariosAsync(id);
            }
            catch (Exception ex)
            {
                throw new DeleteException(ex.Message);
            }
        }
    }
}
