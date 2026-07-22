using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LidyDecorApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "AcessoProdutosClientes")]
    public class ServicosController(IServicoService servicoService) : ControllerBase
    {
        private readonly IServicoService _servicoService = servicoService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicoDTO>>> GetServicos()
        {
            var servicos = await _servicoService.GetServicosAsync();
            return Ok(servicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServicoDTO>> GetServicoById(int id)
        {
            if (id <= 0)
                return BadRequest("ID inválido.");

            var servico = await _servicoService.GetServicoByIdAsync(id);
            return servico == null ? NotFound() : Ok(servico);
        }

        [HttpPost]
        public async Task<ActionResult<ServicoDTO>> AddServico([FromBody] ServicoDTO servico)
        {
            if (servico == null || string.IsNullOrWhiteSpace(servico.Servico))
                return BadRequest("O nome do serviço é obrigatório.");

            var novoServico = await _servicoService.AddServicoAsync(servico);
            return Ok(novoServico);
        }

        [HttpPut]
        [HttpPatch]
        public async Task<ActionResult<ServicoDTO>> UpdateServico([FromBody] ServicoDTO servico)
        {
            if (servico == null || servico.Id <= 0 || string.IsNullOrWhiteSpace(servico.Servico))
                return BadRequest("Dados de serviço inválidos.");

            var servicoAtualizado = await _servicoService.UpdateServicoAsync(servico);
            return Ok(servicoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteServico(int id)
        {
            if (id <= 0)
                return BadRequest("ID inválido.");

            await _servicoService.DeleteServicoAsync(id);
            return Ok();
        }
    }
}
