using System.Text.Json.Serialization;

namespace LidyDecorApp.Application.DTOs
{
    public class ProdutosOrcamentosDTO
    {
        public int Id { get; set; }
        public int ProdutosId { get; set; }
        public int OrcamentosId { get; set; }

        [JsonIgnore]
        public ProdutosDTO? Produtos { get; set; }
        [JsonIgnore]
        public OrcamentosDTO? Orcamentos { get; set; }
    }
}
