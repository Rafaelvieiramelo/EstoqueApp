using AutoMapper;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Exceptions;
using LidyDecorApp.Application.Interfaces;
using LidyDecorApp.Domain;
using LidyDecorApp.Domain.Interfaces;

namespace LidyDecorApp.Application.Services
{
    public class ClientesService(IClientesRepository clientesRepository, IMapper mapper) : IClientesService
    {
        private readonly IClientesRepository _clientesRepository = clientesRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ClientesDTO>> GetClientesAsync()
        {
            var Clientess = await _clientesRepository.GetClientessAsync();
            var ClientessDto = _mapper.Map<IEnumerable<ClientesDTO>>(Clientess);

            return ClientessDto;
        }

        public async Task<ClientesDTO> GetClientesByIdAsync(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id), "Id não pode ser nulo ou zero");

            try
            {
                var clientes = await _clientesRepository.GetClientesByIdAsync(id);
                return _mapper.Map<ClientesDTO>(clientes);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new InvalidOperationException("Erro ao mapear o cliente para DTO.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao buscar o cliente.", ex);
            }
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
