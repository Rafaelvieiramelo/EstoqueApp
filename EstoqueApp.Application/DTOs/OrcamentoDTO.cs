using LidyDecorApp.Domain;
using System.Text.Json.Serialization;
namespace LidyDecorApp.Application.DTOs
{
    public class OrcamentosDTO
    {
        public int Id { get; set; }
        public int TipoEventoId { get; set; }
        public int ClientesId { get; set; }
        public string? Numero { get; set; }
        public DateOnly Data { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly? DataEvento { get; set; }
        public string? Observacoes { get; set; }
        public decimal ValorTotal { get; set; }

        public TipoEventoDTO? TipoEvento { get; set; }
        public ClientesDTO? Clientes { get; set; }
        public ICollection<ProdutosOrcamentosDTO> ProdutosOrcamentos { get; set; } = new List<ProdutosOrcamentosDTO>();
    }
}
