namespace EstoqueApp.Web.Models
{
    public class OrcamentoModel
    {
        public int Id { get; set; }
        public int TipoEventoId { get; set; }
        public int ClienteId { get; set; }
        public string Numero { get; set; }
        public DateOnly Data { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly? DataEvento { get; set; }
        public string? Observacoes { get; set; }
        public decimal ValorTotal { get; set; }

        public TipoEnventoModel? TipoEvento { get; set; }
        public ClienteModel? Cliente { get; set; }
        public List<ProdutoModel>? Produtos { get; set; }
    }

    public class TipoEnventoModel
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
    }
}

