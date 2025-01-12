using EstoqueApp.Infrastructure.Attributes;

namespace EstoqueApp.Application.DTOs
{
    public class CategoriaDTO
    {
        [SwaggerExclude]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
