using AutoMapper;
using Moq;
using LidyDecorApp.Domain.Interfaces;
using LidyDecorApp.Application.Services;
using LidyDecorApp.Domain;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Exceptions;

namespace SeuProjeto.Tests.Services
{
    public class ClientesServiceTests
    {
        private readonly Mock<IClientesRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ClientesService _service;

        public ClientesServiceTests()
        {
            _repositoryMock = new Mock<IClientesRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new ClientesService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetClientesAsync_DeveRetornarListaDeClientesDTO()
        {
            var clientes = new List<Clientes> { new Clientes() };
            var clientesDto = new List<ClientesDTO> { new ClientesDTO() };

            _repositoryMock.Setup(r => r.GetClientessAsync()).ReturnsAsync(clientes);
            _mapperMock.Setup(m => m.Map<IEnumerable<ClientesDTO>>(clientes)).Returns(clientesDto);

            var resultado = await _service.GetClientesAsync();

            Assert.Equal(clientesDto, resultado);
        }

        [Fact]
        public async Task GetClientesByIdAsync_IdZero_DeveLancarExcecao()
        {
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetClientesByIdAsync(0));
            Assert.Equal("Id não pode ser nulo ou zero (Parameter 'id')", ex.Message);
        }

        [Fact]
        public async Task GetClientesByIdAsync_MapeamentoFalha_DeveLancarExcecao()
        {
            _repositoryMock.Setup(r => r.GetClientesByIdAsync(1)).ReturnsAsync(new Clientes());
            _mapperMock.Setup(m => m.Map<ClientesDTO>(It.IsAny<Clientes>())).Throws(new AutoMapperMappingException("Erro"));

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.GetClientesByIdAsync(1));
        }

        [Fact]
        public async Task AddClientesAsync_DeveRetornarClienteDTO()
        {
            var clientesDto = new ClientesDTO();
            var clientes = new Clientes();

            _mapperMock.Setup(m => m.Map<Clientes>(clientesDto)).Returns(clientes);
            _mapperMock.Setup(m => m.Map<ClientesDTO>(clientes)).Returns(clientesDto);

            var resultado = await _service.AddClientesAsync(clientesDto);

            Assert.Equal(clientesDto, resultado);
        }

        [Fact]
        public async Task AddClientesAsync_EmCasoDeErro_DeveRetornarDTOVazio()
        {
            _mapperMock.Setup(m => m.Map<Clientes>(It.IsAny<ClientesDTO>())).Throws<Exception>();

            var resultado = await _service.AddClientesAsync(new ClientesDTO());

            Assert.NotNull(resultado);
            Assert.IsType<ClientesDTO>(resultado);
        }

        [Fact]
        public async Task UpdateClientesAsync_Sucesso_DeveRetornarMesmoDTO()
        {
            var dto = new ClientesDTO();

            _repositoryMock.Setup(r => r.UpdateClientesAsync(It.IsAny<Clientes>())).Returns(Task.CompletedTask);

            var resultado = await _service.UpdateClientesAsync(dto);

            Assert.Equal(dto, resultado);
        }

        [Fact]
        public async Task UpdateClientesAsync_Erro_DeveRetornarDTOVazio()
        {
            _repositoryMock.Setup(r => r.UpdateClientesAsync(It.IsAny<Clientes>())).Throws<Exception>();

            var resultado = await _service.UpdateClientesAsync(new ClientesDTO());

            Assert.NotNull(resultado);
            Assert.IsType<ClientesDTO>(resultado);
        }

        [Fact]
        public async Task DeleteClientesAsync_Erro_DeveLancarDeleteException()
        {
            _repositoryMock.Setup(r => r.DeleteClientesAsync(1)).Throws(new Exception("Erro"));

            var ex = await Assert.ThrowsAsync<DeleteException>(() => _service.DeleteClientesAsync(1));
            Assert.Equal("Erro", ex.Message);
        }
    }
}
