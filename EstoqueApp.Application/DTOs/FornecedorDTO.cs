using EstoqueApp.Infrastructure.Attributes;

namespace EstoqueApp.Application.DTOs
{
    public class FornecedorDTO
    {
        [SwaggerExclude]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
    }
}
