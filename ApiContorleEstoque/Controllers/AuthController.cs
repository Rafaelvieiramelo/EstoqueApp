using EstoqueApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static EstoqueApp.API.Model.AuthModel;

namespace EstoqueApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IUsuarioService usuarioService, IJwtTokenService jwtTokenService)
        {
            _usuarioService = usuarioService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            if (string.IsNullOrEmpty(userLogin.Email) || string.IsNullOrEmpty(userLogin.Senha))
                return BadRequest("Username and password must be provided.");

            var user = await _usuarioService.GetUsuarioByEmailSenhaAsync(userLogin.Email);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            if (!BCrypt.Net.BCrypt.Verify(userLogin.Senha, user.SenhaHash))
                return Unauthorized("Invalid username or password.");

            var token = _jwtTokenService.GenerateToken(user.Email, user.Role);

            return Ok(new { token });
        }
    }
}