using EstoqueApp.Domain;
using EstoqueApp.Domain.Interfaces;

namespace EstoqueApp.Application 
{
    public class CadastrarProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public CadastrarProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task ExecutarAsync(Produto produto)
        {
            await _produtoRepository.AddProdutoAsync(produto);
        }
    }
}

