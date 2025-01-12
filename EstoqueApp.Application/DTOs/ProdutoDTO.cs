using EstoqueApp.Infrastructure.Attributes;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace EstoqueApp.Application.DTOs
{
    public class ProdutoDTO
    {
        [SwaggerExclude]
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int CategoriaId { get; set; }
        public int FornecedorId { get; set; }
    }
}
