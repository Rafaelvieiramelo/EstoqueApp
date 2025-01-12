using EstoqueApp.Application.DTOs;
using EstoqueApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet("GetCategorias")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias()
        {
            var categoria = await _categoriaService.GetCategoriasAsync();
            return Ok(categoria);
        }

        [HttpGet("GeCategoriaById")]
        public async Task<ActionResult<CategoriaDTO>> GeCategoriaById(int id)
        {
            var categoria = await _categoriaService.GetCategoriaByIdAsync(id);
            return Ok(categoria);
        }

        [HttpPost("AddCategoria")]
        public async Task<ActionResult<CategoriaDTO>> AddCategoria(CategoriaDTO categoria)
        {
            var categoriaNovo = await _categoriaService.AddCategoriaAsync(categoria);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(categoriaNovo);

            if (objetoDefault)
                return BadRequest("Erro ao adicionar categoria");

            return Ok(categoriaNovo);
        }

        [HttpPatch("UpdateCategoria")]
        public async Task<ActionResult<CategoriaDTO>> UpdateCategoria(CategoriaDTO categoria)
        {
            var categoriaAtualizado = await _categoriaService.UpdateCategoriaAsync(categoria);
            var objetoDefault = Shared.ObjectValidator.IsObjectDefault(categoriaAtualizado);

            if (objetoDefault)
                return BadRequest("Erro ao editar categoria");

            return Ok(categoriaAtualizado);
        }

        [HttpPatch("DeleteCategoria")]
        public async Task<ActionResult> DeleteCategoria(int id)
        {
            try
            {
                await _categoriaService.DeleteCategoriaAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao Deletar categoria:{ex.Message}");            
            }
        }
    }
}
