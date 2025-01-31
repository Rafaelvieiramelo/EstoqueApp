using EstoqueApp.Application.DTOs;
using EstoqueApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrcamentosController : ControllerBase
    {
        private readonly IOrcamentoService _orcamentoService;

        public OrcamentosController(IOrcamentoService orcamentoService)
        {
            _orcamentoService = orcamentoService;
        }
        
        [HttpGet("GetTiposEvento")]
        public async Task<ActionResult<IEnumerable<TipoEventoDTO>>> GetTiposEvento()
        {
            var tiposEventos = await _orcamentoService.GetTiposEventoAsync();
            return Ok(tiposEventos);
        }

        [HttpGet("GetOrcamentos")]
        public async Task<ActionResult<IEnumerable<OrcamentoDTO>>> GetOrcamentos()
        {
            var orcamentos = await _orcamentoService.GetOrcamentosAsync();
            return Ok(orcamentos);
        }

        [HttpGet("GetOrcamentosById")]
        public async Task<ActionResult<OrcamentoDTO>> GetOrcamentosById(int id)
        {
            var orcamentos = await _orcamentoService.GetOrcamentoByIdAsync(id);
            return Ok(orcamentos);
        }

        [HttpPost("AddOrcamentoAsync")]
        public async Task<ActionResult<OrcamentoDTO>> AddOrcamentoAsync(OrcamentoDTO orcamento)
        {
            var orcamentosNovo = await _orcamentoService.AddOrcamentoAsync(orcamento);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(orcamentosNovo);

            if (objetoDefault)
                return BadRequest("Erro ao adicionar orcamento");

            return Ok(orcamentosNovo);
        }

        [HttpPatch("UpdateOrcamentoAsync")]
        public async Task<ActionResult<OrcamentoDTO>> UpdateOrcamentoAsync(OrcamentoDTO orcamento)
        {
            var orcamentosAtualizado = await _orcamentoService.UpdateOrcamentoAsync(orcamento);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(orcamentosAtualizado);

            if (objetoDefault)
                return BadRequest("Erro ao editar orcamento");

            return Ok(orcamentosAtualizado);
        }

        [HttpPatch("DeleteOrcamentoAsync")]
        public async Task<ActionResult> DeleteOrcamentoAsync(int id)
        {
            try
            {
                await _orcamentoService.DeleteOrcamentoAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao Deletar orcamento:{ex.Message}");            
            }
        }
    }
}
