using FluentValidation;
using LidyDecorApp.Application.DTOs;

namespace LidyDecorApp.Application.Validators
{
    public class OrcamentoDTOValidator : AbstractValidator<OrcamentosDTO>
    {
        public OrcamentoDTOValidator()
        {
            RuleFor(o => o.TipoEventoId)
                .GreaterThan(0).WithMessage("O Tipo Evento deve ser selecionado.");

            RuleFor(o => o.ClientesId)
                .GreaterThan(0).WithMessage("O Cliente deve ser selecionado.");

            RuleFor(o => o.DataEvento)
                .GreaterThanOrEqualTo(o => o.Data)
                .When(o => o.DataEvento.HasValue)
                .WithMessage("A Data do Evento deve ser maior ou igual à Data.");

            RuleFor(o => o.ValorTotal)
                .GreaterThanOrEqualTo(0).WithMessage("O Valor Total deve ser maior ou igual a zero.");

            RuleForEach(o => o.ProdutosOrcamentos)
                .SetValidator(new ProdutosOrcamentosDTOValidator());
        }
    }

    public class ProdutosOrcamentosDTOValidator : AbstractValidator<ProdutosOrcamentosDTO>
    {
        public ProdutosOrcamentosDTOValidator()
        {
            RuleFor(p => p.ProdutosId)
                .GreaterThan(0).WithMessage("Os Produtos devem ser informados");
        }
    }
}