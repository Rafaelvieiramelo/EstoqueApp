using FluentValidation;
using LidyDecorApp.Application.DTOs;

namespace LidyDecorApp.Application.Validators
{
    public class UsuarioWriteDTOValidator : AbstractValidator<UsuariosBaseDTO>
    {
        public UsuarioWriteDTOValidator()
        {
            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("O campo Nome � obrigat�rio.")
                .MaximumLength(100).WithMessage("O campo Nome deve ter no m�ximo 100 caracteres.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O campo Email � obrigat�rio.")
                .EmailAddress().WithMessage("O campo Email deve conter um endere�o de email v�lido.")
                .MaximumLength(150).WithMessage("O campo Email deve ter no m�ximo 150 caracteres.");

            RuleFor(u => u.Role)
                .NotEmpty().WithMessage("O campo Role � obrigat�rio.")
                .MaximumLength(50).WithMessage("O campo Role deve ter no m�ximo 50 caracteres.");
        }
    }
}