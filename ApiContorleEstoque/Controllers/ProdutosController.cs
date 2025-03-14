using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LidyDecorApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private const string id = "{id}";
        private readonly IProdutosService _produtosService;

        public ProdutosController(IProdutosService produtosService)
        {
            _produtosService = produtosService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutosDTO>>> GetProdutos()
        {
            var produtos = await _produtosService.GetProdutosAsync();
            return Ok(produtos);
        }

        [HttpGet(id)]
        public async Task<ActionResult<ProdutosDTO>> GetProdutosById(int id)
        {
            var produtos = await _produtosService.GetProdutosByIdAsync(id);
            return Ok(produtos);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutosDTO>> AddProdutosAsync(ProdutosDTO produtos)
        {
            var produtosNovo = await _produtosService.AddProdutosAsync(produtos);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(produtosNovo);

            if (objetoDefault)
                return BadRequest("Erro ao adicionar produtos");

            return Ok(produtosNovo);
        }

        [HttpPatch]
        public async Task<ActionResult<ProdutosDTO>> UpdateProdutosAsync(ProdutosDTO produtos)
        {
            var produtosAtualizado = await _produtosService.UpdateProdutosAsync(produtos);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(produtosAtualizado);

            if (objetoDefault)
                return BadRequest("Erro ao editar produtos");

            return Ok(produtosAtualizado);
        }

        [HttpDelete(id)]
        public async Task<ActionResult> DeleteProdutosAsync(int id)
        {
            try
            {
                await _produtosService.DeleteProdutosAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao Deletar produtos:{ex.Message}");            
            }
        }
    }
}
