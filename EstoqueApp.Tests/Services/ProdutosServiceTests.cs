using AutoMapper;
using Moq;
using Xunit;
using LidyDecorApp.Domain.Interfaces;
using LidyDecorApp.Application.Services;
using LidyDecorApp.Domain;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Exceptions;

namespace EstoqueApp.Tests.Services;

public class ProdutosServiceTests
{
    private readonly Mock<IProdutosRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ProdutosService _service;

    public ProdutosServiceTests()
    {
        _repositoryMock = new Mock<IProdutosRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new ProdutosService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetProdutosAsync_ShouldReturnProdutosDTOList()
    {
        // Arrange
        var produtos = new List<Produtos> { new Produtos() };
        var produtosDto = new List<ProdutosDTO> { new ProdutosDTO() };

        _repositoryMock.Setup(r => r.GetProdutossAsync()).ReturnsAsync(produtos);
        _mapperMock.Setup(m => m.Map<IEnumerable<ProdutosDTO>>(produtos)).Returns(produtosDto);

        // Act
        var result = await _service.GetProdutosAsync();

        // Assert
        Assert.Equal(produtosDto, result);
    }

    [Fact]
    public async Task GetProdutosByIdAsync_ShouldThrowException_WhenIdIsZero()
    {
        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetProdutosByIdAsync(0));
        Assert.Equal("Id não pode ser nulo ou zero (Parameter 'id')", ex.Message);
    }

    [Fact]
    public async Task GetProdutosByIdAsync_ShouldReturnProdutosDTO()
    {
        // Arrange
        var produto = new Produtos();
        var produtoDto = new ProdutosDTO();

        _repositoryMock.Setup(r => r.GetProdutosByIdAsync(1)).ReturnsAsync(produto);
        _mapperMock.Setup(m => m.Map<ProdutosDTO>(produto)).Returns(produtoDto);

        // Act
        var result = await _service.GetProdutosByIdAsync(1);

        // Assert
        Assert.Equal(produtoDto, result);
    }

    [Fact]
    public async Task AddProdutosAsync_ShouldReturnProdutosDTO()
    {
        // Arrange
        var produtoDto = new ProdutosDTO();
        var produto = new Produtos();

        _mapperMock.Setup(m => m.Map<Produtos>(produtoDto)).Returns(produto);
        _mapperMock.Setup(m => m.Map<ProdutosDTO>(produto)).Returns(produtoDto);

        // Act
        var result = await _service.AddProdutosAsync(produtoDto);

        // Assert
        Assert.Equal(produtoDto, result);
    }

    [Fact]
    public async Task AddProdutosAsync_ShouldReturnEmptyDTO_WhenExceptionOccurs()
    {
        // Arrange
        _mapperMock.Setup(m => m.Map<Produtos>(It.IsAny<ProdutosDTO>())).Throws<Exception>();

        // Act
        var result = await _service.AddProdutosAsync(new ProdutosDTO());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProdutosDTO>(result);
    }

    [Fact]
    public async Task UpdateProdutosAsync_ShouldReturnUpdatedProdutosDTO()
    {
        // Arrange
        var produtoDto = new ProdutosDTO();

        _repositoryMock.Setup(r => r.UpdateProdutosAsync(It.IsAny<Produtos>())).Returns(Task.CompletedTask);

        // Act
        var result = await _service.UpdateProdutosAsync(produtoDto);

        // Assert
        Assert.Equal(produtoDto, result);
    }

    [Fact]
    public async Task UpdateProdutosAsync_ShouldReturnEmptyDTO_WhenExceptionOccurs()
    {
        // Arrange
        _repositoryMock.Setup(r => r.UpdateProdutosAsync(It.IsAny<Produtos>())).Throws<Exception>();

        // Act
        var result = await _service.UpdateProdutosAsync(new ProdutosDTO());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProdutosDTO>(result);
    }

    [Fact]
    public async Task DeleteProdutosAsync_ShouldThrowDeleteException_WhenErrorOccurs()
    {
        // Arrange
        _repositoryMock.Setup(r => r.DeleteProdutosAsync(1)).Throws(new Exception("Erro"));

        // Act & Assert
        var ex = await Assert.ThrowsAsync<DeleteException>(() => _service.DeleteProdutosAsync(1));
        Assert.Equal("Erro", ex.Message);
    }
}