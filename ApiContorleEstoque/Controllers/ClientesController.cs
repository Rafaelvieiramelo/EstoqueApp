using EstoqueApp.Application.DTOs;
using EstoqueApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("GetClientes")]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientes()
        {
            var clientes = await _clienteService.GetClientesAsync();
            return Ok(clientes);
        }

        [HttpGet("GetClientesById")]
        public async Task<ActionResult<ClienteDTO>> GetClientesById(int id)
        {
            var clientes = await _clienteService.GetClienteByIdAsync(id);
            return Ok(clientes);
        }

        [HttpPost("AddClienteAsync")]
        public async Task<ActionResult<ClienteDTO>> AddClienteAsync(ClienteDTO cliente)
        {
            var clientesNovo = await _clienteService.AddClienteAsync(cliente);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(clientesNovo);

            if (objetoDefault)
                return BadRequest("Erro ao adicionar cliente");

            return Ok(clientesNovo);
        }

        [HttpPatch("UpdateClienteAsync")]
        public async Task<ActionResult<ClienteDTO>> UpdateClienteAsync(ClienteDTO cliente)
        {
            var clientesAtualizado = await _clienteService.UpdateClienteAsync(cliente);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(clientesAtualizado);

            if (objetoDefault)
                return BadRequest("Erro ao editar cliente");

            return Ok(clientesAtualizado);
        }

        [HttpPatch("DeleteClienteAsync")]
        public async Task<ActionResult> DeleteClienteAsync(int id)
        {
            try
            {
                await _clienteService.DeleteClienteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao Deletar cliente:{ex.Message}");            
            }
        }
    }
}
