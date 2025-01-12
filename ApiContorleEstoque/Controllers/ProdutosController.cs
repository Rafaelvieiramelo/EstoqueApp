using EstoqueApp.Application.DTOs;
using EstoqueApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet("GetProdutos")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos()
        {
            var produtos = await _produtoService.GetProdutosAsync();
            return Ok(produtos);
        }

        [HttpGet("GetProdutosById")]
        public async Task<ActionResult<ProdutoDTO>> GetProdutosById(int id)
        {
            var produtos = await _produtoService.GetProdutoByIdAsync(id);
            return Ok(produtos);
        }

        [HttpPost("AddProdutoAsync")]
        public async Task<ActionResult<ProdutoDTO>> AddProdutoAsync(ProdutoDTO produto)
        {
            var produtosNovo = await _produtoService.AddProdutoAsync(produto);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(produtosNovo);

            if (objetoDefault)
                return BadRequest("Erro ao adicionar produto");

            return Ok(produtosNovo);
        }

        [HttpPatch("UpdateProdutoAsync")]
        public async Task<ActionResult<ProdutoDTO>> UpdateProdutoAsync(ProdutoDTO produto)
        {
            var produtosAtualizado = await _produtoService.UpdateProdutoAsync(produto);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(produtosAtualizado);

            if (objetoDefault)
                return BadRequest("Erro ao editar produto");

            return Ok(produtosAtualizado);
        }

        [HttpPatch("DeleteProdutoAsync")]
        public async Task<ActionResult> DeleteProdutoAsync(int id)
        {
            try
            {
                await _produtoService.DeleteProdutoAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao Deletar produto:{ex.Message}");            
            }
        }
    }
}
