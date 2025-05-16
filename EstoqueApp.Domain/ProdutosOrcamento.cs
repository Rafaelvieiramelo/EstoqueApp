
namespace LidyDecorApp.Domain
{
    public class ProdutosOrcamento
    {
        public int Id { get; set; }
        public int ProdutosId { get; set; }
        public int OrcamentosId { get; set; }
        public DateOnly Inclusao { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public Produtos? Produtos { get; set; }
        public Orcamentos? Orcamentos { get; set; }
    }
}