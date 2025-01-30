using AutoMapper;
using EstoqueApp.Application.DTOs;
using EstoqueApp.Application.Exceptions;
using EstoqueApp.Application.Interfaces;
using EstoqueApp.Domain;
using EstoqueApp.Domain.Interfaces;

namespace EstoqueApp.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository, IFornecedorRepository fornecedorRepository,  IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutosAsync()
        {
            var produtos = await _produtoRepository.GetProdutosAsync();
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

            if (produtosDto != null && produtosDto.Any())
            {
                foreach (var produtoDto in produtosDto)
                {
                    var fronecedor = await _fornecedorRepository.GetFornecedorByIdAsync(produtoDto.FornecedorId);
                    var categoria = await _categoriaRepository.GetCategoriaByIdAsync(produtoDto.CategoriaId);
                    
                    produtoDto.NomeFornecedor = fronecedor.Nome;
                    produtoDto.NomeCategoria = categoria.Nome
                        ;
                }
            }

            return produtosDto;
        }

        public async Task<ProdutoDTO> GetProdutoByIdAsync(int id)
        {
            var produto = await _produtoRepository.GetProdutoByIdAsync(id);
            return _mapper.Map<ProdutoDTO>(produto);
        }

        public async Task<ProdutoDTO> AddProdutoAsync(ProdutoDTO produtoDTO)
        {
            try
            {
                var produto = _mapper.Map<Produto>(produtoDTO);
                await _produtoRepository.AddProdutoAsync(produto);

                return _mapper.Map<ProdutoDTO>(produto);
            }
            catch (Exception)
            {
                return new ProdutoDTO();
            }
        }

        public async Task<ProdutoDTO> UpdateProdutoAsync(ProdutoDTO produto)
        {
            try
            {
                await _produtoRepository.UpdateProdutoAsync(_mapper.Map<Produto>(produto));
                return produto;
            }
            catch (Exception)
            {
                return new ProdutoDTO();
            }            
        }

        public async Task DeleteProdutoAsync(int id)
        {
            try
            {
                await _produtoRepository.DeleteProdutoAsync(id);
            }
            catch (Exception ex)
            {
                throw new DeleteException(ex.Message);
            }
        }
    }
}
