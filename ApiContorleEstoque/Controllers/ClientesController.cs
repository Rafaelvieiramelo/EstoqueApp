using FluentValidation;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Interfaces;
using LidyDecorApp.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LidyDecorApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "AcessoProdutosClientes")]
    public class ClientesController(IClientesService clientesService, IValidator<ClientesDTO> validator) : ControllerBase

    {
        private const string id = "{id}";
        private readonly IClientesService _clientesService = clientesService;
        private readonly IValidator<ClientesDTO> _validator = validator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesDTO>>> GetClientes()
        {
            var clientes = await _clientesService.GetClientesAsync();
            return clientes.HasNotValue() ? NotFound() : Ok(clientes);
        }

        [HttpGet(id)]
        public async Task<ActionResult<ClientesDTO>> GetClientesById(int id)
        {
            if (id == 0)
                return BadRequest();
            
            var clientes = await _clientesService.GetClientesByIdAsync(id);
            return clientes == null ? NotFound() :  Ok(clientes);
        }

        [HttpPost]
        public async Task<ActionResult<ClientesDTO>> AddClientesAsync([FromBody] ClientesDTO clientes)
        {
            try
            {
                var validation = await _validator.ValidateAsync(clientes);

                if (!validation.IsValid)
                {
                    var errors = validation.Errors.Select(e => new
                    {
                        Campo = e.PropertyName,
                        Erro = e.ErrorMessage
                    });

                    return BadRequest(errors);
                }

                var clientesNovo = await _clientesService.AddClientesAsync(clientes);

                return Ok(clientesNovo);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        public async Task<ActionResult<ClientesDTO>> UpdateClientesAsync(ClientesDTO clientes)
        {
            try
            {
                var validation = await _validator.ValidateAsync(clientes);

                if (!validation.IsValid)
                {
                    var errors = validation.Errors.Select(e => new
                    {
                        Campo = e.PropertyName,
                        Erro = e.ErrorMessage
                    });

                    return BadRequest(errors);
                }

                var clientesAtualizado = await _clientesService.UpdateClientesAsync(clientes);
                
                if (clientesAtualizado == null)
                    return NotFound();

                return Ok(clientesAtualizado);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete(id)]
        public async Task<ActionResult> DeleteClientesAsync(int id)
        {
            if (id == 0)
                return BadRequest();

            try
            {
                await _clientesService.DeleteClientesAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();            
            }
        }
    }
}
