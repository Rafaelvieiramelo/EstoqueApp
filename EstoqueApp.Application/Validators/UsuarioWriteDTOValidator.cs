using FluentValidation;
using LidyDecorApp.Application.DTOs;

namespace LidyDecorApp.Application.Validators
{
    public class UsuarioWriteDTOValidator : AbstractValidator<UsuariosBaseDTO>
    {
        public UsuarioWriteDTOValidator()
        {
            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("O campo Nome é obrigatório.")
                .MaximumLength(100).WithMessage("O campo Nome deve ter no máximo 100 caracteres.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O campo Email é obrigatório.")
                .EmailAddress().WithMessage("O campo Email deve conter um endereço de email válido.")
                .MaximumLength(150).WithMessage("O campo Email deve ter no máximo 150 caracteres.");

            RuleFor(u => u.Role)
                .NotEmpty().WithMessage("O campo Role é obrigatório.")
                .MaximumLength(50).WithMessage("O campo Role deve ter no máximo 50 caracteres.");
        }
    }
}