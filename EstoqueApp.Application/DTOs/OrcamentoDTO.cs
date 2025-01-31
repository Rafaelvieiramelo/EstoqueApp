namespace EstoqueApp.Application.DTOs
{
    public class OrcamentoDTO
    {
        public int Id { get; set; }
        public int TipoEventoId { get; set; }
        public int ClienteId { get; set; }
        public string Numero { get; set; }
        public DateOnly Data { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly? DataEvento { get; set; }
        public string? Observacoes { get; set; }
        public decimal ValorTotal { get; set; }

        public TipoEventoDTO? TipoEvento { get; set; }
        public ClienteDTO? Cliente { get; set; }
        public List<ProdutoDTO>? Produtos { get; set; }
    }
}
