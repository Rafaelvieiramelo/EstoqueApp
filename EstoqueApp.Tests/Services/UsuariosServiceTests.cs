using AutoMapper;
using Moq;
using Xunit;
using LidyDecorApp.Domain.Interfaces;
using LidyDecorApp.Application.Services;
using LidyDecorApp.Domain;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Exceptions;

namespace EstoqueApp.Tests.Services;

public class UsuariosServiceTests
{
    private readonly Mock<IUsuariosRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UsuariosService _service;

    public UsuariosServiceTests()
    {
        _repositoryMock = new Mock<IUsuariosRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new UsuariosService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetUsuariosByEmailSenhaAsync_ShouldReturnUsuarioReadDTO()
    {
        // Arrange
        var usuario = new Usuarios();
        var usuarioDto = new UsuarioReadDTO();

        _repositoryMock.Setup(r => r.GetUsuariosByEmailAsync("email@example.com")).ReturnsAsync(usuario);
        _mapperMock.Setup(m => m.Map<UsuarioReadDTO>(usuario)).Returns(usuarioDto);

        // Act
        var result = await _service.GetUsuariosByEmailSenhaAsync("email@example.com");

        // Assert
        Assert.Equal(usuarioDto, result);
    }

    [Fact]
    public async Task GetUsuariosAsync_ShouldReturnUsuarioReadDTOList()
    {
        // Arrange
        var usuarios = new List<Usuarios> { new Usuarios() };
        var usuariosDto = new List<UsuarioReadDTO> { new UsuarioReadDTO() };

        _repositoryMock.Setup(r => r.GetUsuariossAsync()).ReturnsAsync(usuarios);
        _mapperMock.Setup(m => m.Map<IEnumerable<UsuarioReadDTO>>(usuarios)).Returns(usuariosDto);

        // Act
        var result = await _service.GetUsuariosAsync();

        // Assert
        Assert.Equal(usuariosDto, result);
    }

    [Fact]
    public async Task GetUsuariosByIdAsync_ShouldThrowException_WhenIdIsZero()
    {
        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetUsuariosByIdAsync(0));
        Assert.Equal("Id não pode ser nulo ou zero (Parameter 'id')", ex.Message);
    }

    [Fact]
    public async Task GetUsuariosByIdAsync_ShouldReturnUsuarioReadDTO()
    {
        // Arrange
        var usuario = new Usuarios();
        var usuarioDto = new UsuarioReadDTO();

        _repositoryMock.Setup(r => r.GetUsuariosByIdAsync(1)).ReturnsAsync(usuario);
        _mapperMock.Setup(m => m.Map<UsuarioReadDTO>(usuario)).Returns(usuarioDto);

        // Act
        var result = await _service.GetUsuariosByIdAsync(1);

        // Assert
        Assert.Equal(usuarioDto, result);
    }

    [Fact]
    public async Task AddUsuariosAsync_ShouldReturnUsuarioWriteDTO()
    {
        // Arrange
        var usuarioDto = new UsuarioWriteDTO();
        var usuario = new Usuarios();

        _mapperMock.Setup(m => m.Map<Usuarios>(usuarioDto)).Returns(usuario);
        _mapperMock.Setup(m => m.Map<UsuarioWriteDTO>(usuario)).Returns(usuarioDto);

        // Act
        var result = await _service.AddUsuariosAsync(usuarioDto);

        // Assert
        Assert.Equal(usuarioDto, result);
    }

    [Fact]
    public async Task AddUsuariosAsync_ShouldReturnEmptyDTO_WhenExceptionOccurs()
    {
        // Arrange
        _mapperMock.Setup(m => m.Map<Usuarios>(It.IsAny<UsuarioWriteDTO>())).Throws<Exception>();

        // Act
        var result = await _service.AddUsuariosAsync(new UsuarioWriteDTO());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<UsuarioWriteDTO>(result);
    }

    [Fact]
    public async Task UpdateUsuariosAsync_ShouldReturnUpdatedUsuarioWriteDTO()
    {
        // Arrange
        var usuarioDto = new UsuarioWriteDTO();

        _repositoryMock.Setup(r => r.UpdateUsuariosAsync(It.IsAny<Usuarios>())).Returns(Task.CompletedTask);

        // Act
        var result = await _service.UpdateUsuariosAsync(usuarioDto);

        // Assert
        Assert.Equal(usuarioDto, result);
    }

    [Fact]
    public async Task UpdateUsuariosAsync_ShouldReturnEmptyDTO_WhenExceptionOccurs()
    {
        // Arrange
        _repositoryMock.Setup(r => r.UpdateUsuariosAsync(It.IsAny<Usuarios>())).Throws<Exception>();

        // Act
        var result = await _service.UpdateUsuariosAsync(new UsuarioWriteDTO());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<UsuarioWriteDTO>(result);
    }

    [Fact]
    public async Task DeleteUsuariosAsync_ShouldThrowDeleteException_WhenErrorOccurs()
    {
        // Arrange
        _repositoryMock.Setup(r => r.DeleteUsuariosAsync(1)).Throws(new Exception("Erro"));

        // Act & Assert
        var ex = await Assert.ThrowsAsync<DeleteException>(() => _service.DeleteUsuariosAsync(1));
        Assert.Equal("Erro", ex.Message);
    }
}