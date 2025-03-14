using AutoMapper;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Exceptions;
using LidyDecorApp.Application.Interfaces;
using LidyDecorApp.Domain;
using LidyDecorApp.Domain.Interfaces;

namespace LidyDecorApp.Application.Services
{
    public class ClientesService : IClientesService
    {
        private readonly IClientesRepository _clientesRepository;
        private readonly IMapper _mapper;

        public ClientesService(IClientesRepository clientesRepository, IMapper mapper)
        {
            _clientesRepository = clientesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientesDTO>> GetClientesAsync()
        {
            var Clientess = await _clientesRepository.GetClientessAsync();
            var ClientessDto = _mapper.Map<IEnumerable<ClientesDTO>>(Clientess);

            return ClientessDto;
        }

        public async Task<ClientesDTO> GetClientesByIdAsync(int id)
        {
            var clientes = await _clientesRepository.GetClientesByIdAsync(id);
            return _mapper.Map<ClientesDTO>(clientes);
        }

        public async Task<ClientesDTO> AddClientesAsync(ClientesDTO clientesDTO)
        {
            try
            {
                var clientes = _mapper.Map<Clientes>(clientesDTO);
                await _clientesRepository.AddClientesAsync(clientes);

                return _mapper.Map<ClientesDTO>(clientes);
            }
            catch (Exception)
            {
                return new ClientesDTO();
            }
        }

        public async Task<ClientesDTO> UpdateClientesAsync(ClientesDTO clientes)
        {
            try
            {
                await _clientesRepository.UpdateClientesAsync(_mapper.Map<Clientes>(clientes));
                return clientes;
            }
            catch (Exception)
            {
                return new ClientesDTO();
            }            
        }

        public async Task DeleteClientesAsync(int id)
        {
            try
            {
                await _clientesRepository.DeleteClientesAsync(id);
            }
            catch (Exception ex)
            {
                throw new DeleteException(ex.Message);
            }
        }
    }
}
