using System.Text.Json.Serialization;

namespace LidyDecorApp.Application.DTOs
{
    public class UsuarioReadDTO : UsuariosBaseDTO
    {
        [JsonIgnore]
        public string SenhaHash { get; set; }
    }
}
