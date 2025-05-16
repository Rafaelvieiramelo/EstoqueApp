using FluentValidation;
using LidyDecorApp.API.Controllers;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LidyDecorApp.Tests.Controllers
{
    public class ClientesControllerTests
    {
        private readonly Mock<IClientesService> _mockService;
        private readonly ClientesController _controller;

        public ClientesControllerTests()
        {
            _mockService = new Mock<IClientesService>();
            _controller = new ClientesController(_mockService.Object, null);
        }

        [Fact]
        public async Task GetClientes_ReturnsOk_WhenClienteExists()
        {
            // Arrange
            var cliente = new List<ClientesDTO>
            {
                new() { Id = 1, Nome = "123" },
                new() { Id = 1, Nome = "321" }
            };

            _mockService.Setup(s => s.GetClientesAsync()).ReturnsAsync(cliente);

            // Act
            var result = await _controller.GetClientes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(cliente, okResult.Value);
        }

        [Fact]
        public async Task GetCliente_ReturnsNotFound_WhenClienteDoesNotExist()
        {
            // Arrange
            List<ClientesDTO> value = [];
            _mockService.Setup(s => s.GetClientesAsync()).ReturnsAsync(value);

            // Act
            var result = await _controller.GetClientes();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetClienteById_ReturnsOk_WhenClienteExists()
        {
            // Arrange
            var cleinte = new ClientesDTO { Id = 1, Nome = "Teste" };
            _mockService.Setup(s => s.GetClientesByIdAsync(1)).ReturnsAsync(cleinte);

            // Act
            var result = await _controller.GetClientesById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(cleinte, okResult.Value);
        }

        [Fact]
        public async Task GetClienteById_ReturnsNotFound_WhenClienteDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetClientesByIdAsync(1)).ReturnsAsync((ClientesDTO)null);

            // Act
            var result = await _controller.GetClientesById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddClienteAsync_ReturnsOk_WhenClienteIsAddedSuccessfully()
        {
            // Arrange
            var cliente = new ClientesDTO { Nome = "Teste" };
            var validationResult = new FluentValidation.Results.ValidationResult();

            var mockValidator = new Mock<IValidator<ClientesDTO>>();
            mockValidator.Setup(v => v.ValidateAsync(cliente, default)).ReturnsAsync(validationResult);

            _mockService.Setup(s => s.AddClientesAsync(It.IsAny<ClientesDTO>())).ReturnsAsync(cliente);

            var controller = new ClientesController(_mockService.Object, mockValidator.Object);

            // Act
            var result = await controller.AddClientesAsync(cliente);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(cliente, okResult.Value);
        }

        [Fact]
        public async Task AddClientesAsync_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var cliente = new ClientesDTO { Nome = "123" };
            var validationResult = new FluentValidation.Results.ValidationResult();

            var mockValidator = new Mock<IValidator<ClientesDTO>>();
            mockValidator.Setup(v => v.ValidateAsync(cliente, default)).ReturnsAsync(validationResult);

            _mockService.Setup(s => s.AddClientesAsync(It.IsAny<ClientesDTO>()))
                         .ThrowsAsync(new Exception("Erro ao adicionar cliente"));

            var controller = new ClientesController(_mockService.Object, mockValidator.Object);

            // Act
            var result = await controller.AddClientesAsync(cliente);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task UpdateClientesAsync_ReturnsOk_WhenClienteIsUpdatedSuccessfully()
        {
            // Arrange
            var cliente = new ClientesDTO { Id = 1, Nome = "123" };
            var validationResult = new FluentValidation.Results.ValidationResult();

            var mockValidator = new Mock<IValidator<ClientesDTO>>();
            mockValidator.Setup(v => v.ValidateAsync(cliente, default)).ReturnsAsync(validationResult);

            _mockService.Setup(s => s.UpdateClientesAsync(It.IsAny<ClientesDTO>())).ReturnsAsync(cliente);

            var controller = new ClientesController(_mockService.Object, mockValidator.Object);

            // Act
            var result = await controller.UpdateClientesAsync(cliente);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(cliente, okResult.Value);
        }

        [Fact]
        public async Task UpdateClientesAsync_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var cliente = new ClientesDTO { Id = 1, Numero = "123" };
            var validationResult = new FluentValidation.Results.ValidationResult();

            var mockValidator = new Mock<IValidator<ClientesDTO>>();
            mockValidator.Setup(v => v.ValidateAsync(cliente, default)).ReturnsAsync(validationResult);

            _mockService.Setup(s => s.UpdateClientesAsync(It.IsAny<ClientesDTO>()))
                        .ThrowsAsync(new Exception("Erro ao atualizar cliente"));

            var controller = new ClientesController(_mockService.Object, mockValidator.Object);

            // Act
            var result = await controller.UpdateClientesAsync(cliente);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task DeleteClientesAsync_ReturnsOk_WhenDeletionIsSuccessful()
        {
            // Arrange
            var id = 1;
            _mockService.Setup(s => s.DeleteClientesAsync(id)).Returns(Task.CompletedTask);

            var controller = new ClientesController(_mockService.Object, null);

            // Act
            var result = await controller.DeleteClientesAsync(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteClientesAsync_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var id = 1;
            _mockService.Setup(s => s.DeleteClientesAsync(id)).ThrowsAsync(new Exception("Erro ao deletar"));

            var controller = new ClientesController(_mockService.Object, null);

            // Act
            var result = await controller.DeleteClientesAsync(id);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteClientesAsync_ReturnsBadRequest_WhenIdIsZero()
        {
            // Arrange
            var id = 0;

            var controller = new ClientesController(_mockService.Object, null);

            // Act
            var result = await controller.DeleteClientesAsync(id);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
