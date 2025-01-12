using EstoqueApp.Domain.Interfaces;
using EstoqueApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Infrastructure.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly EstoqueDbContext _context;

        public ProdutoRepository(EstoqueDbContext context)
        {
            _context = context;
        }

        public async Task AddProdutoAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produto>> GetProdutosAsync()
        {
            return await _context.Produtos
                                 .Include(p => p.Categoria)
                                 .Include(p => p.Fornecedor)
                                 .ToListAsync();
        }

        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            return await _context.Produtos
                                 .Include(p => p.Categoria)
                                 .Include(p => p.Fornecedor)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateProdutoAsync(Produto produto)
        {
            var produtoExistente = await _context.Produtos.FindAsync(produto.Id);

            if (produtoExistente != null)
            {
                produtoExistente.Nome = produto.Nome;
                produtoExistente.Descricao = produto.Descricao;
                produtoExistente.Quantidade = produto.Quantidade;
                produtoExistente.PrecoUnitario = produto.PrecoUnitario;
                produtoExistente.CategoriaId = produto.CategoriaId;
                produtoExistente.FornecedorId = produto.FornecedorId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProdutoAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Produto>> GetProdutosByCategoriaAsync(int categoriaId)
        {
            return await _context.Produtos
                                 .Include(p => p.Categoria)
                                 .Include(p => p.Fornecedor)
                                 .Where(p => p.CategoriaId == categoriaId)
                                 .ToListAsync();
        }
    }
}
