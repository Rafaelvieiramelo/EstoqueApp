using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LidyDecorApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private const string id = "{id}";
        private readonly IClientesService _clientesService;

        public ClientesController(IClientesService clientesService)
        {
            _clientesService = clientesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesDTO>>> GetClientes()
        {
            var clientes = await _clientesService.GetClientesAsync();
            return Ok(clientes);
        }

        [HttpGet(id)]
        public async Task<ActionResult<ClientesDTO>> GetClientesById(int id)
        {
            var clientes = await _clientesService.GetClientesByIdAsync(id);
            return Ok(clientes);
        }

        [HttpPost]
        public async Task<ActionResult<ClientesDTO>> AddClientesAsync([FromBody] ClientesDTO clientes)
        {
            var clientesNovo = await _clientesService.AddClientesAsync(clientes);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(clientesNovo);

            if (objetoDefault)
                return BadRequest("Erro ao adicionar clientes");

            return Ok(clientesNovo);
        }

        [HttpPatch]
        public async Task<ActionResult<ClientesDTO>> UpdateClientesAsync(ClientesDTO clientes)
        {
            var clientesAtualizado = await _clientesService.UpdateClientesAsync(clientes);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(clientesAtualizado);

            if (objetoDefault)
                return BadRequest("Erro ao editar clientes");

            return Ok(clientesAtualizado);
        }

        [HttpDelete(id)]
        public async Task<ActionResult> DeleteClientesAsync(int id)
        {
            try
            {
                await _clientesService.DeleteClientesAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao Deletar clientes:{ex.Message}");            
            }
        }
    }
}
