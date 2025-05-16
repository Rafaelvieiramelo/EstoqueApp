using FluentValidation.TestHelper;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Validators;

namespace LidyDecorApp.Tests.Validators
{
    public class ClientesDTOValidatorTests
    {
        private readonly ClientesDTOValidator _validator;

        public ClientesDTOValidatorTests()
        {
            _validator = new ClientesDTOValidator();
        }

        [Fact]
        public void Deve_retornar_erro_se_nome_estiver_vazio()
        {
            var model = new ClientesDTO { Nome = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Nome);
        }

        [Fact]
        public void Deve_retornar_erro_se_email_estiver_vazio()
        {
            var model = new ClientesDTO { Email = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Deve_retornar_erro_se_email_for_invalido()
        {
            var model = new ClientesDTO { Email = "email_invalido" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Deve_retornar_erro_se_telefone_estiver_vazio()
        {
            var model = new ClientesDTO { Telefone = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Telefone);
        }

        [Fact]
        public void Deve_retornar_erro_se_cpfcnpj_estiver_vazio()
        {
            var model = new ClientesDTO { CpfCnpj = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.CpfCnpj);
        }

        [Fact]
        public void Deve_retornar_erro_se_logradouro_estiver_vazio()
        {
            var model = new ClientesDTO { Logradouro = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Logradouro);
        }

        [Fact]
        public void Deve_retornar_erro_se_numero_estiver_vazio()
        {
            var model = new ClientesDTO { Numero = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Numero);
        }

        [Fact]
        public void Deve_retornar_erro_se_bairro_estiver_vazio()
        {
            var model = new ClientesDTO { Bairro = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Bairro);
        }

        [Fact]
        public void Deve_retornar_erro_se_cidade_estiver_vazio()
        {
            var model = new ClientesDTO { Cidade = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Cidade);
        }

        [Fact]
        public void Deve_retornar_erro_se_estado_estiver_vazio()
        {
            var model = new ClientesDTO { Estado = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Estado);
        }

        [Fact]
        public void Deve_retornar_erro_se_cep_estiver_vazio()
        {
            var model = new ClientesDTO { Cep = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Cep);
        }

        [Fact]
        public void Deve_passar_na_validacao_com_dados_validos()
        {
            var model = new ClientesDTO
            {
                Nome = "Cliente Teste",
                Email = "cliente@teste.com",
                Telefone = "999999999",
                CpfCnpj = "12345678900",
                Logradouro = "Rua A",
                Numero = "123",
                Bairro = "Centro",
                Cidade = "Cidade X",
                Estado = "Estado Y",
                Cep = "00000000"
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
