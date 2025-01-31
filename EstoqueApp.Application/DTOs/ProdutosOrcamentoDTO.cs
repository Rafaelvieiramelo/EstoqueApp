namespace EstoqueApp.Application.DTOs
{
    public class ProdutosOrcamentoDTO
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int OrcamentoId { get; set; }
        public DateOnly Inclusao { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public ProdutoDTO? Produto { get; set; }
        public OrcamentoDTO? Orcamento { get; set; }
    }
}
