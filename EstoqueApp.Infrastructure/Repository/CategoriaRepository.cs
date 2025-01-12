using EstoqueApp.Domain.Interfaces;
using EstoqueApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Infrastructure.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly EstoqueDbContext _context;

        public CategoriaRepository(EstoqueDbContext context)
        {
            _context = context;
        }

        public async Task AddCategoriaAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria> GetCategoriaByIdAsync(int id)
        {
            return await _context.Categorias
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateCategoriaAsync(Categoria categoria)
        {
            var categoriaExistente = await _context.Categorias.FindAsync(categoria.Id);

            if (categoriaExistente != null)
            {
                categoriaExistente.Nome = categoria.Nome;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCategoriaAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
        }
    }
}
