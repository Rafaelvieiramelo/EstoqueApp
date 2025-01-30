using AutoMapper;
using EstoqueApp.Application.DTOs;
using EstoqueApp.Application.Exceptions;
using EstoqueApp.Application.Interfaces;
using EstoqueApp.Domain;
using EstoqueApp.Domain.Interfaces;

namespace EstoqueApp.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteDTO>> GetClientesAsync()
        {
            var Clientes = await _clienteRepository.GetClientesAsync();
            var ClientesDto = _mapper.Map<IEnumerable<ClienteDTO>>(Clientes);

            return ClientesDto;
        }

        public async Task<ClienteDTO> GetClienteByIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(id);
            return _mapper.Map<ClienteDTO>(cliente);
        }

        public async Task<ClienteDTO> AddClienteAsync(ClienteDTO clienteDTO)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteDTO);
                await _clienteRepository.AddClienteAsync(cliente);

                return _mapper.Map<ClienteDTO>(cliente);
            }
            catch (Exception)
            {
                return new ClienteDTO();
            }
        }

        public async Task<ClienteDTO> UpdateClienteAsync(ClienteDTO cliente)
        {
            try
            {
                await _clienteRepository.UpdateClienteAsync(_mapper.Map<Cliente>(cliente));
                return cliente;
            }
            catch (Exception)
            {
                return new ClienteDTO();
            }            
        }

        public async Task DeleteClienteAsync(int id)
        {
            try
            {
                await _clienteRepository.DeleteClienteAsync(id);
            }
            catch (Exception ex)
            {
                throw new DeleteException(ex.Message);
            }
        }
    }
}
