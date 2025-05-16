using FluentValidation;
using LidyDecorApp.API.Controllers;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LidyDecorApp.Tests.Controllers
{
    public class UsuariosControllerTests
    {
        private readonly Mock<IUsuariosService> _mockService;
        private readonly UsuariosController _controller;

        public UsuariosControllerTests()
        {
            _mockService = new Mock<IUsuariosService>();
            _controller = new UsuariosController(_mockService.Object, null);
        }

        [Fact]
        public async Task GetUsuarios_ReturnsOk_WhenUsuarioExists()
        {
            // Arrange
            var usuarios = new List<UsuarioReadDTO>
            {
                new() { Id = 1, Nome = "123" },
                new() { Id = 1, Nome = "321" }
            };

            _mockService.Setup(s => s.GetUsuariosAsync()).ReturnsAsync(usuarios);

            // Act
            var result = await _controller.GetUsuarios();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(usuarios, okResult.Value);
        }

        [Fact]
        public async Task GetUsuario_ReturnsNotFound_WhenUsuarioDoesNotExist()
        {
            // Arrange
            List<UsuarioReadDTO> value = [];
            _mockService.Setup(s => s.GetUsuariosAsync()).ReturnsAsync(value);

            // Act
            var result = await _controller.GetUsuarios();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetUsuarioById_ReturnsOk_WhenUsuarioExists()
        {
            // Arrange
            var cleinte = new UsuarioReadDTO { Id = 1, Nome = "Teste" };
            _mockService.Setup(s => s.GetUsuariosByIdAsync(1)).ReturnsAsync(cleinte);

            // Act
            var result = await _controller.GeUsuariosById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(cleinte, okResult.Value);
        }

        [Fact]
        public async Task GetUsuarioById_ReturnsNotFound_WhenUsuarioDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetUsuariosByIdAsync(1)).ReturnsAsync((UsuarioReadDTO)null);

            // Act
            var result = await _controller.GeUsuariosById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddUsuarioAsync_ReturnsOk_WhenUsuarioIsAddedSuccessfully()
        {
            // Arrange
            var usuarios = new UsuarioWriteDTO { Nome = "Teste", SenhaHash = "123456" };
            var validationResult = new FluentValidation.Results.ValidationResult();

            var mockValidator = new Mock<IValidator<UsuarioWriteDTO>>();
            mockValidator.Setup(v => v.ValidateAsync(usuarios, default)).ReturnsAsync(validationResult);

            _mockService.Setup(s => s.AddUsuariosAsync(It.IsAny<UsuarioWriteDTO>())).ReturnsAsync(usuarios);

            var controller = new UsuariosController(_mockService.Object, mockValidator.Object);

            // Act
            var result = await controller.AddUsuarios(usuarios);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(usuarios, okResult.Value);
        }

        [Fact]
        public async Task AddUsuariosAsync_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var usuarios = new UsuarioWriteDTO { Nome = "Teste" };
            var validationResult = new FluentValidation.Results.ValidationResult();

            var mockValidator = new Mock<IValidator<UsuarioWriteDTO>>();
            mockValidator.Setup(v => v.ValidateAsync(usuarios, default)).ReturnsAsync(validationResult);

            _mockService.Setup(s => s.AddUsuariosAsync(It.IsAny<UsuarioWriteDTO>()))
                         .ThrowsAsync(new Exception("Erro ao adicionar usuarios"));

            var controller = new UsuariosController(_mockService.Object, mockValidator.Object);

            // Act
            var result = await controller.AddUsuarios(usuarios);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task UpdateUsuariosAsync_ReturnsOk_WhenUsuarioIsUpdatedSuccessfully()
        {
            // Arrange
            var usuarios = new UsuarioWriteDTO { Id = 1, Nome = "Teste", SenhaHash = "123456" };
            var validationResult = new FluentValidation.Results.ValidationResult();

            var mockValidator = new Mock<IValidator<UsuarioWriteDTO>>();
            mockValidator.Setup(v => v.ValidateAsync(usuarios, default)).ReturnsAsync(validationResult);

            _mockService.Setup(s => s.UpdateUsuariosAsync(It.IsAny<UsuarioWriteDTO>())).ReturnsAsync(usuarios);

            var controller = new UsuariosController(_mockService.Object, mockValidator.Object);

            // Act
            var result = await controller.UpdateUsuarios(usuarios);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(usuarios, okResult.Value);
        }

        [Fact]
        public async Task UpdateUsuariosAsync_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var usuarios = new UsuarioWriteDTO { Id = 1, Nome = "123" };
            var validationResult = new FluentValidation.Results.ValidationResult();

            var mockValidator = new Mock<IValidator<UsuarioWriteDTO>>();
            mockValidator.Setup(v => v.ValidateAsync(usuarios, default)).ReturnsAsync(validationResult);

            _mockService.Setup(s => s.UpdateUsuariosAsync(It.IsAny<UsuarioWriteDTO>()))
                        .ThrowsAsync(new Exception("Erro ao atualizar usuarios"));

            var controller = new UsuariosController(_mockService.Object, mockValidator.Object);

            // Act
            var result = await controller.UpdateUsuarios(usuarios);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task DeleteUsuariosAsync_ReturnsOk_WhenDeletionIsSuccessful()
        {
            // Arrange
            var id = 1;
            _mockService.Setup(s => s.DeleteUsuariosAsync(id)).Returns(Task.CompletedTask);

            var controller = new UsuariosController(_mockService.Object, null);

            // Act
            var result = await controller.DeleteUsuarios(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteUsuariosAsync_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var id = 1;
            _mockService.Setup(s => s.DeleteUsuariosAsync(id)).ThrowsAsync(new Exception("Erro ao deletar"));

            var controller = new UsuariosController(_mockService.Object, null);

            // Act
            var result = await controller.DeleteUsuarios(id);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteUsuariosAsync_ReturnsBadRequest_WhenIdIsZero()
        {
            // Arrange
            var id = 0;

            var controller = new UsuariosController(_mockService.Object, null);

            // Act
            var result = await controller.DeleteUsuarios(id);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
