namespace LidyDecorApp.Web.Models
{
    public class UsuariosModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senhaHash { get; set; }
        public string role { get; set; }
        public bool ativo { get; set; } = true;
    }
}
