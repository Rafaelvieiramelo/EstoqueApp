using System.Text.Json.Serialization;

namespace LidyDecorApp.Web.Models
{
    public class ViaCepModel
    {
        [JsonPropertyName("cep")]
        public string? Cep { get; set; }

        [JsonPropertyName("logradouro")]
        public string? Logradouro { get; set; }

        [JsonPropertyName("complemento")]
        public string? Complemento { get; set; }

        [JsonPropertyName("bairro")]
        public string? Bairro { get; set; }

        [JsonPropertyName("localidade")]
        public string? Cidade { get; set; }

        [JsonPropertyName("uf")]
        public string? Estado { get; set; }

        [JsonPropertyName("erro")]
        public object? Erro { get; set; }

        public bool IsErro => Erro != null && (Erro.ToString()?.Equals("true", System.StringComparison.OrdinalIgnoreCase) == true || Erro.ToString()?.Equals("True", System.StringComparison.OrdinalIgnoreCase) == true);
    }
}
