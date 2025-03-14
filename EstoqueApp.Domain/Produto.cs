namespace LidyDecorApp.Domain
{
    public class Produtos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        public ICollection<ProdutosOrcamento> ProdutosOrcamento { get; set; } = new List<ProdutosOrcamento>();
    }
}    