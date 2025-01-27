namespace EstoqueApp.Web.Models
{
    public class ProdutoModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public int quantidade { get; set; }
        public decimal precoUnitario { get; set; }
        public int categoriaId { get; set; }
        public int fornecedorId { get; set; }
        public string nomeCategoria { get; set; }
        public string nomeFornecedor { get; set; }
    }
}
