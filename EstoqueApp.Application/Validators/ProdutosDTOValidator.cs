using FluentValidation;
using LidyDecorApp.Application.DTOs;

namespace LidyDecorApp.Application.Validators
{
    public class ProdutosDTOValidator : AbstractValidator<ProdutosDTO>
    {
        public ProdutosDTOValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O campo Nome é obrigatório.")
                .MaximumLength(100).WithMessage("O campo Nome deve ter no máximo 100 caracteres.");
        }
    }
}