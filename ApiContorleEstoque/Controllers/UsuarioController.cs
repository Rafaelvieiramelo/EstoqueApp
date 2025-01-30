using EstoqueApp.Application.DTOs;
using EstoqueApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("GetUsuarios")]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var usuario = await _usuarioService.GetUsuariosAsync();
            return Ok(usuario);
        }

        [HttpGet("GeUsuarioById")]
        public async Task<ActionResult<UsuarioDTO>> GeUsuarioById(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            return Ok(usuario);
        }

        [HttpPost("AddUsuario")]
        public async Task<ActionResult<UsuarioDTO>> AddUsuario(UsuarioDTO usuario)
        {
            usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.SenhaHash);
            var usuarioNovo = await _usuarioService.AddUsuarioAsync(usuario);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(usuarioNovo);

            if (objetoDefault)
                return BadRequest("Erro ao adicionar usuario");

            return Ok(usuarioNovo);
        }

        [HttpPatch("UpdateUsuario")]
        public async Task<ActionResult<UsuarioDTO>> UpdateUsuario(UsuarioDTO usuario)
        {
            usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuario.SenhaHash);
            var usuarioAtualizado = await _usuarioService.UpdateUsuarioAsync(usuario);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(usuarioAtualizado);

            if (objetoDefault)
                return BadRequest("Erro ao editar usuario");

            return Ok(usuarioAtualizado);
        }

        [HttpPatch("DeleteUsuario")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            try
            {
                await _usuarioService.DeleteUsuarioAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao Deletar usuario:{ex.Message}");            
            }
        }
    }
}
