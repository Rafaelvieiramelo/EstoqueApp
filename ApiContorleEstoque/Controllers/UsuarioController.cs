using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LidyDecorApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "AcessoTotal")]
    public class UsuariosController : ControllerBase
    {
        private const string id = "{id}";
        private readonly IUsuariosService _usuariosService;

        public UsuariosController(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioReadDTO>>> GetUsuarios()
        {
            var usuarios = await _usuariosService.GetUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpGet(id)]
        public async Task<ActionResult<UsuarioReadDTO>> GeUsuariosById(int id)
        {
            var usuarios = await _usuariosService.GetUsuariosByIdAsync(id);
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioWriteDTO>> AddUsuarios(UsuarioWriteDTO usuarios)
        {
            usuarios.SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuarios.SenhaHash);
            var usuariosNovo = await _usuariosService.AddUsuariosAsync(usuarios);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(usuariosNovo);

            if (objetoDefault)
                return BadRequest("Erro ao adicionar usuarios");

            return Ok(usuariosNovo);
        }

        [HttpPatch]
        public async Task<ActionResult<UsuarioWriteDTO>> UpdateUsuarios(UsuarioWriteDTO usuarios)
        {
            usuarios.SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuarios.SenhaHash);
            var usuariosAtualizado = await _usuariosService.UpdateUsuariosAsync(usuarios);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(usuariosAtualizado);

            if (objetoDefault)
                return BadRequest("Erro ao editar usuarios");

            return Ok(usuariosAtualizado);
        }

        [HttpDelete(id)]
        public async Task<ActionResult> DeleteUsuarios(int id)
        {
            try
            {
                await _usuariosService.DeleteUsuariosAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao Deletar usuarios:{ex.Message}");            
            }
        }
    }
}
