namespace LidyDecorApp.Web.Models
{
    public class ProdutossOrcamentosModel
    {
        public int Id { get; set; }
        public int ProdutosId { get; set; }
        public int OrcamentosId { get; set; }
        public DateOnly Inclusao { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public ProdutosModel? Produtos { get; set; }
        public OrcamentosModel? Orcamentos { get; set; }
    }
}
