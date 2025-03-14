using System.Text.Json.Serialization;

namespace LidyDecorApp.Web.Models
{
    public class OrcamentosModel
    {
        public int Id { get; set; }
        public int TipoEventoId { get; set; }
        public int ClientesId { get; set; }
        public string Numero { get; set; }
        public DateOnly Data { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly DataEvento { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Observacoes { get; set; }
        public decimal ValorTotal { get; set; }

        [JsonIgnore]
        public Tipoevento TipoEvento { get; set; }

        [JsonIgnore]
        public ClientesModel Clientes { get; set; }

        public List<ProdutosOrcamentos> ProdutosOrcamentos { get; set; } = [];
    }

    public class Tipoevento
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
    }
        
    public class ProdutosOrcamentos
    {
        public int Id { get; set; }
        public int ProdutosId { get; set; }
        public int OrcamentosId { get; set; }

        [JsonIgnore]
        public ProdutosModel Produtos { get; set; }
        [JsonIgnore]
        public object Orcamentos { get; set; }
    }
}

