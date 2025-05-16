using FluentValidation;
using LidyDecorApp.API.Controllers;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LidyDecorApp.Tests.Controllers
{
    public class ProdutosControllerTests
    {
        private readonly Mock<IProdutosService> _mockService;
        private readonly ProdutosController _controller;

        public ProdutosControllerTests()
        {
            _mockService = new Mock<IProdutosService>();
            _controller = new ProdutosController(_mockService.Object, null);
        }

        [Fact]
        public async Task GetProdutos_ReturnsOk_WhenProdutoExists()
        {
            // Arrange
            var produtos = new List<ProdutosDTO>
            {
                new() { Id = 1, Nome = "123" },
                new() { Id = 1, Nome = "321" }
            };

            _mockService.Setup(s => s.GetProdutosAsync()).ReturnsAsync(produtos);

            // Act
            var result = await _controller.GetProdutos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(produtos, okResult.Value);
        }

        [Fact]
        public async Task GetProduto_ReturnsNotFound_WhenProdutoDoesNotExist()
        {
            // Arrange
            List<ProdutosDTO> value = [];
            _mockService.Setup(s => s.GetProdutosAsync()).ReturnsAsync(value);

            // Act
            var result = await _controller.GetProdutos();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetProdutoById_ReturnsOk_WhenProdutoExists()
        {
            // Arrange
            var cleinte = new ProdutosDTO { Id = 1, Nome = "Teste" };
            _mockService.Setup(s => s.GetProdutosByIdAsync(1)).ReturnsAsync(cleinte);

            // Act
            var result = await _controller.GetProdutosById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(cleinte, okResult.Value);
        }

        [Fact]
        public async Task GetProdutoById_ReturnsNotFound_WhenProdutoDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetProdutosByIdAsync(1)).ReturnsAsync((ProdutosDTO)null);

            // Act
            var result = await _controller.GetProdutosById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddProdutoAsync_ReturnsOk_WhenProdutoIsAddedSuccessfully()
        {
            // Arrange
            var produtos = new ProdutosDTO { Nome = "Teste" };
            var validationResult = new FluentValidation.Results.ValidationResult();

            var mockValidator = new Mock<IValidator<ProdutosDTO>>();
            mockValidator.Setup(v => v.ValidateAsync(produtos, default)).ReturnsAsync(validationResult);

            _mockService.Setup(s => s.AddProdutosAsync(It.IsAny<ProdutosDTO>())).ReturnsAsync(produtos);

            var controller = new ProdutosController(_mockService.Object, mockValidator.Object);

            // Act
            var result = await controller.AddProdutosAsync(produtos);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(produtos, okResult.Value);
        }

        [Fact]
        public async Task AddProdutosAsync_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var produtos = new ProdutosDTO { Nome = "123" };
            var validationResult = new FluentValidation.Results.ValidationResult();

            var mockValidator = new Mock<IValidator<ProdutosDTO>>();
            mockValidator.Setup(v => v.ValidateAsync(produtos, default)).ReturnsAsync(validationResult);

            _mockService.Setup(s => s.AddProdutosAsync(It.IsAny<ProdutosDTO>()))
                         .ThrowsAsync(new Exception("Erro ao adicionar produtos"));

            var controller = new ProdutosController(_mockService.Object, mockValidator.Object);

            // Act
            var result = await controller.AddProdutosAsync(produtos);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task UpdateProdutosAsync_ReturnsOk_WhenProdutoIsUpdatedSuccessfully()
        {
            // Arrange
            var produtos = new ProdutosDTO { Id = 1, Nome = "123" };
            var validationResult = new FluentValidation.Results.ValidationResult();

            var mockValidator = new Mock<IValidator<ProdutosDTO>>();
            mockValidator.Setup(v => v.ValidateAsync(produtos, default)).ReturnsAsync(validationResult);

            _mockService.Setup(s => s.UpdateProdutosAsync(It.IsAny<ProdutosDTO>())).ReturnsAsync(produtos);

            var controller = new ProdutosController(_mockService.Object, mockValidator.Object);

            // Act
            var result = await controller.UpdateProdutosAsync(produtos);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(produtos, okResult.Value);
        }

        [Fact]
        public async Task UpdateProdutosAsync_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var produtos = new ProdutosDTO { Id = 1, Nome = "123" };
            var validationResult = new FluentValidation.Results.ValidationResult();

            var mockValidator = new Mock<IValidator<ProdutosDTO>>();
            mockValidator.Setup(v => v.ValidateAsync(produtos, default)).ReturnsAsync(validationResult);

            _mockService.Setup(s => s.UpdateProdutosAsync(It.IsAny<ProdutosDTO>()))
                        .ThrowsAsync(new Exception("Erro ao atualizar produtos"));

            var controller = new ProdutosController(_mockService.Object, mockValidator.Object);

            // Act
            var result = await controller.UpdateProdutosAsync(produtos);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task DeleteProdutosAsync_ReturnsOk_WhenDeletionIsSuccessful()
        {
            // Arrange
            var id = 1;
            _mockService.Setup(s => s.DeleteProdutosAsync(id)).Returns(Task.CompletedTask);

            var controller = new ProdutosController(_mockService.Object, null);

            // Act
            var result = await controller.DeleteProdutosAsync(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteProdutosAsync_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var id = 1;
            _mockService.Setup(s => s.DeleteProdutosAsync(id)).ThrowsAsync(new Exception("Erro ao deletar"));

            var controller = new ProdutosController(_mockService.Object, null);

            // Act
            var result = await controller.DeleteProdutosAsync(id);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteProdutosAsync_ReturnsBadRequest_WhenIdIsZero()
        {
            // Arrange
            var id = 0;

            var controller = new ProdutosController(_mockService.Object, null);

            // Act
            var result = await controller.DeleteProdutosAsync(id);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
