using AutoMapper;
using EstoqueApp.Application.DTOs;
using EstoqueApp.Application.Exceptions;
using EstoqueApp.Application.Interfaces;
using EstoqueApp.Domain;
using EstoqueApp.Domain.Interfaces;

namespace EstoqueApp.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaDTO>> GetCategoriasAsync()
        {
            var categorias = await _categoriaRepository.GetCategoriasAsync();
            var categoriasDto = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);

            return categoriasDto;
        }

        public async Task<CategoriaDTO> GetCategoriaByIdAsync(int id)
        {
            var categoria = await _categoriaRepository.GetCategoriaByIdAsync(id);
            return _mapper.Map<CategoriaDTO>(categoria);
        }

        public async Task<CategoriaDTO> AddCategoriaAsync(CategoriaDTO CategoriaDTO)
        {
            try
            {
                var categoria = _mapper.Map<Categoria>(CategoriaDTO);
                await _categoriaRepository.AddCategoriaAsync(categoria);

                return _mapper.Map<CategoriaDTO>(categoria);
            }
            catch (Exception)
            {
                return new CategoriaDTO();
            }
        }

        public async Task<CategoriaDTO> UpdateCategoriaAsync(CategoriaDTO categoria)
        {
            try
            {
                await _categoriaRepository.UpdateCategoriaAsync(_mapper.Map<Categoria>(categoria));
                return categoria;
            }
            catch (Exception)
            {
                return new CategoriaDTO();
            }            
        }

        public async Task DeleteCategoriaAsync(int id)
        {
            try
            {
                await _categoriaRepository.DeleteCategoriaAsync(id);
            }
            catch (Exception ex)
            {
                throw new DeleteException(ex.Message);
            }
        }
    }
}
