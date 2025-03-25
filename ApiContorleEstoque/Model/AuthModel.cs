using System.ComponentModel.DataAnnotations;

namespace LidyDecorApp.API.Model
{
    public class AuthModel
    {
        public class UserLogin
        {
            [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
            [EmailAddress(ErrorMessage = "E-mail inválido.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo Senha é obrigatório.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
