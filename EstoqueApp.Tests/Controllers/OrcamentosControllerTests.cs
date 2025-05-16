using FluentValidation;
using LidyDecorApp.API.Controllers;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EstoqueApp.Tests.Controllers;

public class OrcamentosControllerTests
{
    private readonly Mock<IOrcamentosService> _mockService;
    private readonly OrcamentosController _controller;

    public OrcamentosControllerTests()
    {
        _mockService = new Mock<IOrcamentosService>();
        _controller = new OrcamentosController(_mockService.Object, null);
    }

    [Fact]
    public async Task GetOrcamento_ReturnsOk_WhenOrcamentoExists()
    {
        // Arrange
        var orcamento = new List<OrcamentosDTO>
        {
            new() { Id = 1, Numero = "123" },
            new() { Id = 1, Numero = "321" }
        };

        _mockService.Setup(s => s.GetOrcamentosAsync()).ReturnsAsync(orcamento);

        // Act
        var result = await _controller.GetOrcamentos();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(orcamento, okResult.Value);
    }

    [Fact]
    public async Task GetOrcamento_ReturnsNotFound_WhenOrcamentoDoesNotExist()
    {
        // Arrange
        List<OrcamentosDTO> value = [];
        _mockService.Setup(s => s.GetOrcamentosAsync()).ReturnsAsync(value);

        // Act
        var result = await _controller.GetOrcamentos();

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetOrcamentoById_ReturnsOk_WhenOrcamentoExists()
    {
        // Arrange
        var orcamento = new OrcamentosDTO { Id = 1, Numero = "123" };
        _mockService.Setup(s => s.GetOrcamentosByIdAsync(1)).ReturnsAsync(orcamento);

        // Act
        var result = await _controller.GetOrcamentosById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(orcamento, okResult.Value);
    }

    [Fact]
    public async Task GetOrcamentoById_ReturnsNotFound_WhenOrcamentoDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetOrcamentosByIdAsync(1)).ReturnsAsync((OrcamentosDTO)null);

        // Act
        var result = await _controller.GetOrcamentosById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task AddOrcamentosAsync_ReturnsOk_WhenOrcamentoIsAddedSuccessfully()
    {
        // Arrange
        var orcamento = new OrcamentosDTO { Numero = "123" };
        var validationResult = new FluentValidation.Results.ValidationResult();

        var mockValidator = new Mock<IValidator<OrcamentosDTO>>();
        mockValidator.Setup(v => v.ValidateAsync(orcamento, default)).ReturnsAsync(validationResult);

        _mockService.Setup(s => s.GetNumeroUltimoOrcamentosAsync()).ReturnsAsync("123");
        _mockService.Setup(s => s.AddOrcamentosAsync(It.IsAny<OrcamentosDTO>())).ReturnsAsync(orcamento);

        var controller = new OrcamentosController(_mockService.Object, mockValidator.Object);

        // Act
        var result = await controller.AddOrcamentosAsync(orcamento);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(orcamento, okResult.Value);
    }

    [Fact]
    public async Task AddOrcamentosAsync_ReturnsBadRequest_WhenExceptionIsThrown()
    {
        // Arrange
        var orcamento = new OrcamentosDTO { Numero = "123" };
        var validationResult = new FluentValidation.Results.ValidationResult();

        var mockValidator = new Mock<IValidator<OrcamentosDTO>>();
        mockValidator.Setup(v => v.ValidateAsync(orcamento, default)).ReturnsAsync(validationResult);

        _mockService.Setup(s => s.GetNumeroUltimoOrcamentosAsync()).ReturnsAsync("123");
        _mockService.Setup(s => s.AddOrcamentosAsync(It.IsAny<OrcamentosDTO>()))
                     .ThrowsAsync(new Exception("Erro ao adicionar orçamento"));

        var controller = new OrcamentosController(_mockService.Object, mockValidator.Object);

        // Act
        var result = await controller.AddOrcamentosAsync(orcamento);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task UpdateOrcamentosAsync_ReturnsOk_WhenOrcamentoIsUpdatedSuccessfully()
    {
        // Arrange
        var orcamento = new OrcamentosDTO { Id = 1, Numero = "123" };
        var validationResult = new FluentValidation.Results.ValidationResult();

        var mockValidator = new Mock<IValidator<OrcamentosDTO>>();
        mockValidator.Setup(v => v.ValidateAsync(orcamento, default)).ReturnsAsync(validationResult);

        _mockService.Setup(s => s.UpdateOrcamentosAsync(It.IsAny<OrcamentosDTO>())).ReturnsAsync(orcamento);

        var controller = new OrcamentosController(_mockService.Object, mockValidator.Object);

        // Act
        var result = await controller.UpdateOrcamentosAsync(orcamento);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(orcamento, okResult.Value);
    }

    [Fact]
    public async Task UpdateOrcamentosAsync_ReturnsBadRequest_WhenExceptionIsThrown()
    {
        // Arrange
        var orcamento = new OrcamentosDTO { Id = 1, Numero = "123" };
        var validationResult = new FluentValidation.Results.ValidationResult();

        var mockValidator = new Mock<IValidator<OrcamentosDTO>>();
        mockValidator.Setup(v => v.ValidateAsync(orcamento, default)).ReturnsAsync(validationResult);

        _mockService.Setup(s => s.UpdateOrcamentosAsync(It.IsAny<OrcamentosDTO>()))
                    .ThrowsAsync(new Exception("Erro ao atualizar orçamento"));

        var controller = new OrcamentosController(_mockService.Object, mockValidator.Object);

        // Act
        var result = await controller.UpdateOrcamentosAsync(orcamento);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task DeleteOrcamentosAsync_ReturnsOk_WhenDeletionIsSuccessful()
    {
        // Arrange
        var id = 1;
        _mockService.Setup(s => s.DeleteOrcamentosAsync(id)).Returns(Task.CompletedTask);

        var controller = new OrcamentosController(_mockService.Object, null);

        // Act
        var result = await controller.DeleteOrcamentosAsync(id);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteOrcamentosAsync_ReturnsBadRequest_WhenExceptionIsThrown()
    {
        // Arrange
        var id = 1;
        _mockService.Setup(s => s.DeleteOrcamentosAsync(id)).ThrowsAsync(new Exception("Erro ao deletar"));

        var controller = new OrcamentosController(_mockService.Object, null);

        // Act
        var result = await controller.DeleteOrcamentosAsync(id);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteOrcamentosAsync_ReturnsBadRequest_WhenIdIsZero()
    {
        // Arrange
        var id = 0;

        var controller = new OrcamentosController(_mockService.Object, null);

        // Act
        var result = await controller.DeleteOrcamentosAsync(id);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
}