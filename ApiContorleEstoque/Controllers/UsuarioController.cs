using FluentValidation;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Interfaces;
using LidyDecorApp.Application.Services;
using LidyDecorApp.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LidyDecorApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "AcessoTotal")]
    public class UsuariosController(IUsuariosService usuariosService, IValidator<UsuarioWriteDTO> validator) : ControllerBase

    {
        private const string id = "{id}";
        private readonly IUsuariosService _usuariosService = usuariosService;
        private readonly IValidator<UsuarioWriteDTO> _validator = validator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioReadDTO>>> GetUsuarios()
        {
            var usuarios = await _usuariosService.GetUsuariosAsync();
            return usuarios.HasNotValue() ? NotFound() : Ok(usuarios);
        }

        [HttpGet(id)]
        public async Task<ActionResult<UsuarioReadDTO>> GeUsuariosById(int id)
        {
            if (id == 0)
                return BadRequest();

            var usuarios = await _usuariosService.GetUsuariosByIdAsync(id);
            return usuarios == null ? NotFound() : Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioWriteDTO>> AddUsuarios(UsuarioWriteDTO usuarios)
        {
            try
            {
                var validation = await _validator.ValidateAsync(usuarios);

                if (!validation.IsValid)
                {
                    var errors = validation.Errors.Select(e => new
                    {
                        Campo = e.PropertyName,
                        Erro = e.ErrorMessage
                    });

                    return BadRequest(errors);
                }

                usuarios.SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuarios.SenhaHash);
                var usuariosNovo = await _usuariosService.AddUsuariosAsync(usuarios);

                return Ok(usuariosNovo);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        public async Task<ActionResult<UsuarioWriteDTO>> UpdateUsuarios(UsuarioWriteDTO usuarios)
        {
            try
            {
                var validation = await _validator.ValidateAsync(usuarios);

                if (!validation.IsValid)
                {
                    var errors = validation.Errors.Select(e => new
                    {
                        Campo = e.PropertyName,
                        Erro = e.ErrorMessage
                    });

                    return BadRequest(errors);
                }

                usuarios.SenhaHash = BCrypt.Net.BCrypt.HashPassword(usuarios.SenhaHash);
                var usuarioAtualizado = await _usuariosService.UpdateUsuariosAsync(usuarios);
                
                return usuarioAtualizado == null ? NotFound() : Ok(usuarioAtualizado);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete(id)]
        public async Task<ActionResult> DeleteUsuarios(int id)
        {
            if (id == 0)
                return BadRequest();

            try
            {
                await _usuariosService.DeleteUsuariosAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();            
            }
        }
    }
}
