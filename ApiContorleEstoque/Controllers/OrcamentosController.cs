using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LidyDecorApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "AcessoTotal")]
    public class OrcamentosController : ControllerBase
    {
        private const string id = "{id}";
        private readonly IOrcamentosService _orcamentosService;

        public OrcamentosController(IOrcamentosService orcamentosService)
        {
            _orcamentosService = orcamentosService;
        }
        
        [HttpGet("GetTiposEvento")]
        public async Task<ActionResult<IEnumerable<TipoEventoDTO>>> GetTiposEvento()
        {
            var tiposEventos = await _orcamentosService.GetTiposEventoAsync();
            return Ok(tiposEventos);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrcamentosDTO>>> GetOrcamentos()
        {
            var orcamentos = await _orcamentosService.GetOrcamentosAsync();
            return Ok(orcamentos);
        }

        [HttpGet(id)]
        public async Task<ActionResult<OrcamentosDTO>> GetOrcamentosById(int id)
        {
            var orcamentos = await _orcamentosService.GetOrcamentosByIdAsync(id);
            return Ok(orcamentos);
        }

        [HttpPost]
        public async Task<ActionResult<OrcamentosDTO>> AddOrcamentosAsync(OrcamentosDTO orcamentos)
        {
            var numeroUltimoOrcamentos = await _orcamentosService.GetNumeroUltimoOrcamentosAsync();
            var proximoNumero = int.Parse(numeroUltimoOrcamentos) + 1;
            orcamentos.Numero = proximoNumero.ToString();
            var orcamentosNovo = await _orcamentosService.AddOrcamentosAsync(orcamentos);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(orcamentosNovo);

            if (objetoDefault)
                return BadRequest("Erro ao adicionar orcamentos");

            return Ok(orcamentosNovo);
        }

        [HttpPatch]
        public async Task<ActionResult<OrcamentosDTO>> UpdateOrcamentosAsync(OrcamentosDTO orcamentos)
        {
            var orcamentosAtualizado = await _orcamentosService.UpdateOrcamentosAsync(orcamentos);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(orcamentosAtualizado);

            if (objetoDefault)
                return BadRequest("Erro ao editar orcamentos");

            return Ok(orcamentosAtualizado);
        }

        [HttpDelete(id)]
        public async Task<ActionResult> DeleteOrcamentosAsync(int id)
        {
            try
            {
                await _orcamentosService.DeleteOrcamentosAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao Deletar orcamentos:{ex.Message}");            
            }
        }
    }
}
