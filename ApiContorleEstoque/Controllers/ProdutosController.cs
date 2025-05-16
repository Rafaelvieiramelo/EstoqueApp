using FluentValidation;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Interfaces;
using LidyDecorApp.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LidyDecorApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController(IProdutosService produtosService, IValidator<ProdutosDTO> validator) : ControllerBase
    {
        private const string id = "{id}";
        private readonly IProdutosService _produtosService = produtosService;
        private readonly IValidator<ProdutosDTO> _validator = validator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutosDTO>>> GetProdutos()
        {
            var produtos = await _produtosService.GetProdutosAsync();
            return produtos.HasNotValue() ? NotFound() : Ok(produtos);
        }

        [HttpGet(id)]
        public async Task<ActionResult<ProdutosDTO>> GetProdutosById(int id)
        {
            if (id == 0)
                return BadRequest();

            var produtos = await _produtosService.GetProdutosByIdAsync(id);
            return produtos == null ? NotFound() : Ok(produtos);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutosDTO>> AddProdutosAsync(ProdutosDTO produtos)
        {
            try
            {
                var validation = await _validator.ValidateAsync(produtos);

                if (!validation.IsValid)
                {
                    var errors = validation.Errors.Select(e => new
                    {
                        Campo = e.PropertyName,
                        Erro = e.ErrorMessage
                    });

                    return BadRequest(errors);
                }

                var produtosNovo = await _produtosService.AddProdutosAsync(produtos);

                return Ok(produtosNovo);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        public async Task<ActionResult<ProdutosDTO>> UpdateProdutosAsync(ProdutosDTO produtos)
        {
            try
            {
                var validation = await _validator.ValidateAsync(produtos);

                if (!validation.IsValid)
                {
                    var errors = validation.Errors.Select(e => new
                    {
                        Campo = e.PropertyName,
                        Erro = e.ErrorMessage
                    });

                    return BadRequest(errors);
                }

                var produtosAtualizado = await _produtosService.UpdateProdutosAsync(produtos);

                if (produtosAtualizado == null)
                    return NotFound();

                return Ok(produtosAtualizado);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete(id)]
        public async Task<ActionResult> DeleteProdutosAsync(int id)
        {
            if (id == 0)
                return BadRequest();

            try
            {
                await _produtosService.DeleteProdutosAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();            
            }
        }
    }
}
