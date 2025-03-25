using System.ComponentModel.DataAnnotations;

namespace LidyDecorApp.API.Model
{
    public class AuthModel
    {
        public class UserLogin
        {
            [Required(ErrorMessage = "O campo E-mail � obrigat�rio.")]
            [EmailAddress(ErrorMessage = "E-mail inv�lido.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo Senha � obrigat�rio.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
