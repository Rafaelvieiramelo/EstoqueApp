using FluentValidation.TestHelper;
using LidyDecorApp.Application.Validators;
using LidyDecorApp.Application.DTOs;

namespace EstoqueApp.Tests.Validators;

public class OrcamentoDTOValidatorTests
{
    private readonly OrcamentoDTOValidator _validator;

    public OrcamentoDTOValidatorTests()
    {
        _validator = new OrcamentoDTOValidator();
    }

    [Fact]
    public void Should_HaveError_When_TipoEventoIdIsZero()
    {
        // Arrange
        var model = new OrcamentosDTO { TipoEventoId = 0 };

        // Act & Assert
        _validator.TestValidate(model).ShouldHaveValidationErrorFor(o => o.TipoEventoId)
            .WithErrorMessage("O Tipo Evento deve ser selecionado.");
    }

    [Fact]
    public void Should_HaveError_When_ClientesIdIsZero()
    {
        // Arrange
        var model = new OrcamentosDTO { ClientesId = 0 };

        // Act & Assert
        _validator.TestValidate(model).ShouldHaveValidationErrorFor(o => o.ClientesId)
            .WithErrorMessage("O Cliente deve ser selecionado.");
    }

    [Fact]
    public void Should_HaveError_When_DataEventoIsBeforeData()
    {
        // Arrange
        var model = new OrcamentosDTO
        {
            Data = new DateOnly(2025, 5, 16),
            DataEvento = new DateOnly(2025, 5, 15)
        };

        // Act & Assert
        _validator.TestValidate(model).ShouldHaveValidationErrorFor(o => o.DataEvento)
            .WithErrorMessage("A Data do Evento deve ser maior ou igual à Data.");
    }

    [Fact]
    public void Should_NotHaveError_When_DataEventoIsEqualToData()
    {
        // Arrange
        var model = new OrcamentosDTO
        {
            Data = new DateOnly(2025, 5, 16),
            DataEvento = new DateOnly(2025, 5, 16)
        };

        // Act & Assert
        _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(o => o.DataEvento);
    }

    [Fact]
    public void Should_HaveError_When_ValorTotalIsNegative()
    {
        // Arrange
        var model = new OrcamentosDTO { ValorTotal = -1 };

        // Act & Assert
        _validator.TestValidate(model).ShouldHaveValidationErrorFor(o => o.ValorTotal)
            .WithErrorMessage("O Valor Total deve ser maior ou igual a zero.");
    }

    [Fact]
    public void Should_NotHaveError_When_ValorTotalIsZeroOrPositive()
    {
        // Arrange
        var model = new OrcamentosDTO { ValorTotal = 0 };

        // Act & Assert
        _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(o => o.ValorTotal);
    }

    [Fact]
    public void Should_HaveError_When_ProdutosOrcamentosIsInvalid()
    {
        // Arrange
        var model = new OrcamentosDTO
        {
            ProdutosOrcamentos = new List<ProdutosOrcamentosDTO>
            {
                new ProdutosOrcamentosDTO { ProdutosId = 0 }
            }
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor("ProdutosOrcamentos[0].ProdutosId")
            .WithErrorMessage("Os Produtos devem ser informados");
    }
}

public class ProdutosOrcamentosDTOValidatorTests
{
    private readonly ProdutosOrcamentosDTOValidator _validator;

    public ProdutosOrcamentosDTOValidatorTests()
    {
        _validator = new ProdutosOrcamentosDTOValidator();
    }

    [Fact]
    public void Should_HaveError_When_ProdutosIdIsZero()
    {
        // Arrange
        var model = new ProdutosOrcamentosDTO { ProdutosId = 0 };

        // Act & Assert
        _validator.TestValidate(model).ShouldHaveValidationErrorFor(p => p.ProdutosId)
            .WithErrorMessage("Os Produtos devem ser informados");
    }

    [Fact]
    public void Should_NotHaveError_When_ProdutosIdIsValid()
    {
        // Arrange
        var model = new ProdutosOrcamentosDTO { ProdutosId = 1 };

        // Act & Assert
        _validator.TestValidate(model).ShouldNotHaveValidationErrorFor(p => p.ProdutosId);
    }
}