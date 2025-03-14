namespace LidyDecorApp.Domain
{
    public class Orcamentos
    {
        public int Id { get; set; }
        public int TipoEventoId { get; set; }
        public int ClientesId { get; set; }
        public string Numero { get; set; }
        public DateOnly Data { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly? DataEvento { get; set; }
        public string? Observacoes { get; set; }
        public decimal ValorTotal { get; set; }

        public TipoEventos? TipoEvento { get; set; }
        public Clientes? Clientes { get; set; }
        public ICollection<ProdutosOrcamento> ProdutosOrcamento { get; set; } = new List<ProdutosOrcamento>();
    }
}    