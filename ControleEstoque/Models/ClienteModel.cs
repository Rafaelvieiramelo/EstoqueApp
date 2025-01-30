namespace EstoqueApp.Web.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? CpfCnpj { get; set; }
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Cep { get; set; }
    }
}
