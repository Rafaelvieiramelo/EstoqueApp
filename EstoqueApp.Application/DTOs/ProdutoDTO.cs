namespace EstoqueApp.Application.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int CategoriaId { get; set; }
        public int FornecedorId { get; set; }
        public string? NomeCategoria { get; set; }
        public string? NomeFornecedor { get; set; }
    }
}
