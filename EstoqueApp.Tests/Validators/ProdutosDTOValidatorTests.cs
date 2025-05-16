using FluentValidation.TestHelper;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Validators;

namespace LidyDecorApp.Tests.Validators
{
    public class ProdutosDTOValidatorTests
    {
        private readonly ProdutosDTOValidator _validator;

        public ProdutosDTOValidatorTests()
        {
            _validator = new ProdutosDTOValidator();
        }

        [Fact]
        public void Deve_retornar_erro_se_nome_estiver_vazio()
        {
            var model = new ProdutosDTO { Nome = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Nome);
        }
    }
}
