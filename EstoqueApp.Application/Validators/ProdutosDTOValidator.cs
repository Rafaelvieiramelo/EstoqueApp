using FluentValidation;
using LidyDecorApp.Application.DTOs;

namespace LidyDecorApp.Application.Validators
{
    public class ProdutosDTOValidator : AbstractValidator<ProdutosDTO>
    {
        public ProdutosDTOValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O campo Nome � obrigat�rio.")
                .MaximumLength(100).WithMessage("O campo Nome deve ter no m�ximo 100 caracteres.");
        }
    }
}