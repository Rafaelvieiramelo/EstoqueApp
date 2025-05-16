using FluentValidation.TestHelper;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Validators;

namespace LidyDecorApp.Tests.Validators
{
    public class UsuariosDTOValidatorTests
    {
        private readonly UsuarioWriteDTOValidator  _validator;

        public UsuariosDTOValidatorTests()
        {
            _validator = new UsuarioWriteDTOValidator();
        }

        [Fact]
        public void Deve_retornar_erro_se_nome_estiver_vazio()
        {
            var model = new UsuariosBaseDTO { Nome = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Nome);
        }

        [Fact]
        public void Deve_retornar_erro_se_email_estiver_vazio()
        {
            var model = new UsuariosBaseDTO { Email = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Deve_retornar_erro_se_email_for_invalido()
        {
            var model = new UsuariosBaseDTO { Email = "email_invalido" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Deve_retornar_erro_se_Role_estiver_vazio()
        {
            var model = new UsuariosBaseDTO { Role = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Role);
        }
    }
}
