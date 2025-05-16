using FluentValidation;
using LidyDecorApp.Application.DTOs;

namespace LidyDecorApp.Application.Validators
{
    public class ClientesDTOValidator : AbstractValidator<ClientesDTO>
    {
        public ClientesDTOValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail está em um formato inválido.");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório.");

            RuleFor(x => x.CpfCnpj)
                .NotEmpty().WithMessage("O CPF ou CNPJ é obrigatório.");

            RuleFor(x => x.Logradouro)
                .NotEmpty().WithMessage("O logradouro é obrigatório.");

            RuleFor(x => x.Numero)
                .NotEmpty().WithMessage("O número é obrigatório.");

            RuleFor(x => x.Bairro)
                .NotEmpty().WithMessage("O bairro é obrigatório.");

            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("A cidade é obrigatória.");

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("O estado é obrigatório.");

            RuleFor(x => x.Cep)
                .NotEmpty().WithMessage("O CEP é obrigatório.");
        }
    }
}
