using AutoMapper;
using LidyDecorApp.Application.DTOs;
using LidyDecorApp.Application.Exceptions;
using LidyDecorApp.Application.Interfaces;
using LidyDecorApp.Domain;
using LidyDecorApp.Domain.Interfaces;

namespace LidyDecorApp.Application.Services
{
    public class ProdutosService(IProdutosRepository produtosRepository, IMapper mapper) : IProdutosService
    {
        private readonly IProdutosRepository _produtosRepository = produtosRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ProdutosDTO>> GetProdutosAsync()
        {
            var produtoss = await _produtosRepository.GetProdutossAsync();
            var produtossDto = _mapper.Map<IEnumerable<ProdutosDTO>>(produtoss);

            return produtossDto;
        }

        public async Task<ProdutosDTO> GetProdutosByIdAsync(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id), "Id não pode ser nulo ou zero");

            try
            {
                var produtos = await _produtosRepository.GetProdutosByIdAsync(id);
                return _mapper.Map<ProdutosDTO>(produtos);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new InvalidOperationException("Erro ao mapear o produto para DTO.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao buscar o produto.", ex);
            }
        }

        public async Task<ProdutosDTO> AddProdutosAsync(ProdutosDTO produtosDTO)
        {
            try
            {
                var produtos = _mapper.Map<Produtos>(produtosDTO);
                await _produtosRepository.AddProdutosAsync(produtos);

                return _mapper.Map<ProdutosDTO>(produtos);
            }
            catch (Exception)
            {
                return new ProdutosDTO();
            }
        }

        public async Task<ProdutosDTO> UpdateProdutosAsync(ProdutosDTO produtos)
        {
            try
            {
                await _produtosRepository.UpdateProdutosAsync(_mapper.Map<Produtos>(produtos));
                return produtos;
            }
            catch (Exception)
            {
                return new ProdutosDTO();
            }            
        }

        public async Task DeleteProdutosAsync(int id)
        {
            try
            {
                await _produtosRepository.DeleteProdutosAsync(id);
            }
            catch (Exception ex)
            {
                throw new DeleteException(ex.Message);
            }
        }
    }
}
