namespace EstoqueApp.Domain
{
    public class ProdutosOrcamento
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int OrcamentoId { get; set; }
        public DateOnly Inclusao { get; set; } = DateOnly.FromDateTime(DateTime.Now);
       

        public Orcamento? Orcamento { get; set; }
        public Produto? Produto { get; set; }
    }
}    