namespace EstoqueApp.Domain
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}    