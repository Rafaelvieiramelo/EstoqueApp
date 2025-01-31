namespace EstoqueApp.Domain
{
    public class Orcamento
    {
        public int Id { get; set; }
        public int TipoEventoId { get; set; }
        public int ClienteId { get; set; }
        public string Numero { get; set; }
        public DateOnly Data { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly? DataEvento { get; set; }
        public string? Observacoes { get; set; }
        public decimal ValorTotal { get; set; }

        public TipoEvento? TipoEvento { get; set; }
        public Cliente? Cliente { get; set; }
        public ICollection<ProdutosOrcamento> ProdutosOrcamento { get; set; } = new List<ProdutosOrcamento>();
    }
}    