using EstoqueApp.Domain.Interfaces;
using EstoqueApp.Domain;
using Microsoft.EntityFrameworkCore;
using static EstoqueApp.Infrastructure.Repository.ProdutoRepository;

namespace EstoqueApp.Infrastructure.Repository
{
    public class MovimentoEstoqueRepository : IMovimentoEstoqueRepository
    {
        private readonly EstoqueDbContext _context;

        public MovimentoEstoqueRepository(EstoqueDbContext context)
        {
            _context = context;
        }

        public async Task AddMovimentoEstoqueAsync(MovimentoEstoque movimentoEstoque)
        {
            await _context.MovimentosEstoque.AddAsync(movimentoEstoque);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovimentoEstoque>> GetMovimentosEstoqueAsync()
        {
            return await _context.MovimentosEstoque
                                 .Include(m => m.Produto)  // Incluir o produto associado
                                 .ToListAsync();
        }

        public async Task<MovimentoEstoque> GetMovimentoEstoqueByIdAsync(int id)
        {
            return await _context.MovimentosEstoque
                                 .Include(m => m.Produto) // Incluir o produto associado
                                 .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateMovimentoEstoqueAsync(MovimentoEstoque movimentoEstoque)
        {
            var movimentoExistente = await _context.MovimentosEstoque.FindAsync(movimentoEstoque.Id);

            if (movimentoExistente != null)
            {
                movimentoExistente.ProdutoId = movimentoEstoque.ProdutoId;
                movimentoExistente.TipoMovimento = movimentoEstoque.TipoMovimento;
                movimentoExistente.Quantidade = movimentoEstoque.Quantidade;
                movimentoExistente.DataMovimento = movimentoEstoque.DataMovimento;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteMovimentoEstoqueAsync(int id)
        {
            var movimentoEstoque = await _context.MovimentosEstoque.FindAsync(id);

            if (movimentoEstoque != null)
            {
                _context.MovimentosEstoque.Remove(movimentoEstoque);
                await _context.SaveChangesAsync();
            }
        }
    }
}
