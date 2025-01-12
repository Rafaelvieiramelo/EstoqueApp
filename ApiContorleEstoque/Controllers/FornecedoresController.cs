using EstoqueApp.Application.DTOs;
using EstoqueApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FornecedoresController : ControllerBase
    {
        private readonly IFornecedorService _fornecedorService;

        public FornecedoresController(IFornecedorService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }

        [HttpGet("GetFornecedores")]
        public async Task<ActionResult<IEnumerable<FornecedorDTO>>> GetFornecedores()
        {
            var fornecedores = await _fornecedorService.GetFornecedoresAsync();
            return Ok(fornecedores);
        }

        [HttpGet("GefornecedorById")]
        public async Task<ActionResult<FornecedorDTO>> GeFornecedorById(int id)
        {
            var fornecedor = await _fornecedorService.GetFornecedorByIdAsync(id);
            return Ok(fornecedor);
        }

        [HttpPost("Addfornecedor")]
        public async Task<ActionResult<FornecedorDTO>> AddFornecedor(FornecedorDTO fornecedor)
        {
            var fornecedorNovo = await _fornecedorService.AddFornecedorAsync(fornecedor);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(fornecedorNovo);

            if (objetoDefault)
                return BadRequest("Erro ao adicionar fornecedor");

            return Ok(fornecedorNovo);
        }

        [HttpPatch("Updatefornecedor")]
        public async Task<ActionResult<FornecedorDTO>> UpdateFornecedor(FornecedorDTO fornecedor)
        {
            var fornecedorAtualizado = await _fornecedorService.UpdateFornecedorAsync(fornecedor);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(fornecedorAtualizado);

            if (objetoDefault)
                return BadRequest("Erro ao editar fornecedor");

            return Ok(fornecedorAtualizado);
        }

        [HttpPatch("Deletefornecedor")]
        public async Task<ActionResult> DeleteFornecedor(int id)
        {
            try
            {
                await _fornecedorService.DeleteFornecedorAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao Deletar fornecedor:{ex.Message}");            
            }
        }
    }
}
