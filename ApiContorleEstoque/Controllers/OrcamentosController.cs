using FluentValidation;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Interfaces;
using LidyDecorApp.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LidyDecorApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrcamentosController(IOrcamentosService orcamentosService, IValidator<OrcamentosDTO> validator) : ControllerBase
    {
        private const string id = "{id}";
        private readonly IOrcamentosService _orcamentosService = orcamentosService;
        private readonly IValidator<OrcamentosDTO> _validator = validator;

        [HttpGet("GetTiposEvento")]
        public async Task<ActionResult<IEnumerable<TipoEventoDTO>>> GetTiposEvento()
        {
            var tiposEventos = await _orcamentosService.GetTiposEventoAsync();
            return Ok(tiposEventos);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrcamentosDTO>>> GetOrcamentos()
        {
            try
            {
                var orcamentos = await _orcamentosService.GetOrcamentosAsync();

                if (orcamentos.HasNotValue())
                    return NotFound();

                return Ok(orcamentos);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet(id)]
        public async Task<ActionResult<OrcamentosDTO>> GetOrcamentosById(int id)
        {
            if (id == 0)
                return BadRequest();

            var orcamento = await _orcamentosService.GetOrcamentosByIdAsync(id);

            return orcamento == null ? NotFound() : Ok(orcamento);
        }

        [HttpPost]
        public async Task<ActionResult<OrcamentosDTO>> AddOrcamentosAsync(OrcamentosDTO orcamentos)
        {
            try
            {
                var validation = await _validator.ValidateAsync(orcamentos);

                if (!validation.IsValid)
                {
                    var errors = validation.Errors.Select(e => new
                    {
                        Campo = e.PropertyName,
                        Erro = e.ErrorMessage
                    });

                    return BadRequest(errors);
                }

                var numeroUltimoOrcamentos = await _orcamentosService.GetNumeroUltimoOrcamentosAsync();
                var proximoNumero = int.Parse(numeroUltimoOrcamentos) + 1;
                orcamentos.Numero = proximoNumero.ToString();

                var orcamentosNovo = await _orcamentosService.AddOrcamentosAsync(orcamentos);

                return Ok(orcamentosNovo);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        public async Task<ActionResult<OrcamentosDTO>> UpdateOrcamentosAsync(OrcamentosDTO orcamentos)
        {
            try
            {
                var validation = await _validator.ValidateAsync(orcamentos);

                if (!validation.IsValid)
                {
                    var errors = validation.Errors.Select(e => new
                    {
                        Campo = e.PropertyName,
                        Erro = e.ErrorMessage
                    });

                    return BadRequest(errors);
                }

                var orcamentosAtualizado = await _orcamentosService.UpdateOrcamentosAsync(orcamentos);

                if (orcamentosAtualizado == null)
                    return NotFound();

                return Ok(orcamentosAtualizado);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete(id)]
        public async Task<ActionResult> DeleteOrcamentosAsync(int id)
        {
            if (id == 0)
                return BadRequest();

            try
            {
                await _orcamentosService.DeleteOrcamentosAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();            
            }
        }
    }
}
