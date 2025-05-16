using AutoMapper;
using Moq;
using LidyDecorApp.Domain.Interfaces;
using LidyDecorApp.Application.Services;
using LidyDecorApp.Domain;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Exceptions;

namespace EstoqueApp.Tests.Services;

public class OrcamentosServiceTests
{
    private readonly Mock<IOrcamentosRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly OrcamentosService _service;

    public OrcamentosServiceTests()
    {
        _repositoryMock = new Mock<IOrcamentosRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new OrcamentosService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetTiposEventoAsync_ShouldReturnTipoEventoDTOList()
    {
        var tiposEvento = new List<TipoEventos> { new TipoEventos() };
        var tiposEventoDto = new List<TipoEventoDTO> { new TipoEventoDTO() };

        _repositoryMock.Setup(r => r.GetTiposEventoAsync()).ReturnsAsync(tiposEvento);
        _mapperMock.Setup(m => m.Map<IEnumerable<TipoEventoDTO>>(tiposEvento)).Returns(tiposEventoDto);

        var result = await _service.GetTiposEventoAsync();

        Assert.Equal(tiposEventoDto, result);
    }

    [Fact]
    public async Task GetOrcamentosAsync_ShouldReturnOrcamentosDTOList()
    {
        var orcamentos = new List<Orcamentos> { new Orcamentos() };
        var orcamentosDto = new List<OrcamentosDTO> { new OrcamentosDTO() };

        _repositoryMock.Setup(r => r.GetOrcamentossAsync()).ReturnsAsync(orcamentos);
        _mapperMock.Setup(m => m.Map<IEnumerable<OrcamentosDTO>>(orcamentos)).Returns(orcamentosDto);

        var result = await _service.GetOrcamentosAsync();

        Assert.Equal(orcamentosDto, result);
    }

    [Fact]
    public async Task GetNumeroUltimoOrcamentosAsync_ShouldReturnCorrectValue()
    {
        _repositoryMock.Setup(r => r.GetNumeroUltimoOrcamentosAsync()).ReturnsAsync("123");

        var result = await _service.GetNumeroUltimoOrcamentosAsync();

        Assert.Equal("123", result);
    }

    [Fact]
    public async Task GetNumeroUltimoOrcamentosAsync_ShouldReturnZero_WhenNullOrEmpty()
    {
        _repositoryMock.Setup(r => r.GetNumeroUltimoOrcamentosAsync()).ReturnsAsync(string.Empty);

        var result = await _service.GetNumeroUltimoOrcamentosAsync();

        Assert.Equal("0", result);
    }

    [Fact]
    public async Task GetOrcamentosByIdAsync_ShouldThrowException_WhenIdIsZero()
    {
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetOrcamentosByIdAsync(0));
        Assert.Equal("Id não pode ser nulo ou zero (Parameter 'id')", ex.Message);
    }

    [Fact]
    public async Task GetOrcamentosByIdAsync_ShouldReturnOrcamentosDTO()
    {
        var orcamentos = new Orcamentos();
        var orcamentosDto = new OrcamentosDTO();

        _repositoryMock.Setup(r => r.GetOrcamentosByIdAsync(1)).ReturnsAsync(orcamentos);
        _mapperMock.Setup(m => m.Map<OrcamentosDTO>(orcamentos)).Returns(orcamentosDto);

        var result = await _service.GetOrcamentosByIdAsync(1);

        Assert.Equal(orcamentosDto, result);
    }

    [Fact]
    public async Task AddOrcamentosAsync_ShouldReturnOrcamentosDTO()
    {
        var orcamentosDto = new OrcamentosDTO();
        var orcamentos = new Orcamentos();

        _mapperMock.Setup(m => m.Map<Orcamentos>(orcamentosDto)).Returns(orcamentos);
        _mapperMock.Setup(m => m.Map<OrcamentosDTO>(orcamentos)).Returns(orcamentosDto);

        var result = await _service.AddOrcamentosAsync(orcamentosDto);

        Assert.Equal(orcamentosDto, result);
    }

    [Fact]
    public async Task UpdateOrcamentosAsync_ShouldReturnUpdatedOrcamentosDTO()
    {
        var orcamentosDto = new OrcamentosDTO();

        _repositoryMock.Setup(r => r.UpdateOrcamentosAsync(It.IsAny<Orcamentos>())).Returns(Task.CompletedTask);

        var result = await _service.UpdateOrcamentosAsync(orcamentosDto);

        Assert.Equal(orcamentosDto, result);
    }

    [Fact]
    public async Task UpdateOrcamentosAsync_ShouldReturnEmptyDTO_WhenExceptionOccurs()
    {
        _repositoryMock.Setup(r => r.UpdateOrcamentosAsync(It.IsAny<Orcamentos>())).Throws<Exception>();

        var result = await _service.UpdateOrcamentosAsync(new OrcamentosDTO());

        Assert.NotNull(result);
        Assert.IsType<OrcamentosDTO>(result);
    }

    [Fact]
    public async Task DeleteOrcamentosAsync_ShouldThrowDeleteException_WhenErrorOccurs()
    {
        _repositoryMock.Setup(r => r.DeleteOrcamentosAsync(1)).Throws(new Exception("Erro"));

        var ex = await Assert.ThrowsAsync<DeleteException>(() => _service.DeleteOrcamentosAsync(1));
        Assert.Equal("Erro", ex.Message);
    }
}