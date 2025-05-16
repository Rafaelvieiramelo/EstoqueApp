using AutoMapper;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Exceptions;
using LidyDecorApp.Application.Interfaces;
using LidyDecorApp.Domain;
using LidyDecorApp.Domain.Interfaces;

namespace LidyDecorApp.Application.Services
{
    public class UsuariosService(IUsuariosRepository usuariosRepository, IMapper mapper) : IUsuariosService
    {
        private readonly IUsuariosRepository _usuariosRepository = usuariosRepository;
        private readonly IMapper _mapper = mapper;

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
            if (id == 0)
                throw new ArgumentNullException(nameof(id), "Id não pode ser nulo ou zero");

            try
            {
                var usuarios = await _usuariosRepository.GetUsuariosByIdAsync(id);
                return _mapper.Map<UsuarioReadDTO>(usuarios);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new InvalidOperationException("Erro ao mapear o usuario para DTO.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao buscar o usuario.", ex);
            }
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
