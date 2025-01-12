namespace EstoqueApp.Domain
{
    public class MovimentoEstoque
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public string TipoMovimento { get; set; } 
        public int Quantidade { get; set; }
        public DateTime DataMovimento { get; set; }
    }
}