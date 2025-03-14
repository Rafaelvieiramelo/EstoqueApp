using LidyDecorApp.Domain.Interfaces;
using LidyDecorApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace LidyDecorApp.Infrastructure.Repository
{
    public class ProdutosRepository : IProdutosRepository
    {
        private readonly LidyDecorDbContext _context;

        public ProdutosRepository(LidyDecorDbContext context)
        {
            _context = context;
        }

        public async Task AddProdutosAsync(Produtos produtos)
        {
            await _context.Produtos.AddAsync(produtos);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produtos>> GetProdutossAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produtos> GetProdutosByIdAsync(int id)
        {
            return await _context.Produtos.FirstAsync(p => p.Id == id);
        }

        public async Task UpdateProdutosAsync(Produtos produtos)
        {
            var produtosExistente = await _context.Produtos.FindAsync(produtos.Id);

            if (produtosExistente != null)
            {
                produtosExistente.Nome = produtos.Nome;
                produtosExistente.Descricao = produtos.Descricao;
                produtosExistente.Quantidade = produtos.Quantidade;
                produtosExistente.PrecoUnitario = produtos.PrecoUnitario;                

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProdutosAsync(int id)
        {
            var produtos = await _context.Produtos.FindAsync(id);

            if (produtos != null)
            {
                _context.Produtos.Remove(produtos);
                await _context.SaveChangesAsync();
            }
        }
    }
}
