using LidyDecorApp.Domain;
using LidyDecorApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LidyDecorApp.Infrastructure.Repository
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly LidyDecorDbContext _context;

        public ServicoRepository(LidyDecorDbContext context)
        {
            _context = context;
        }

        public async Task AddServicoAsync(Servico servico)
        {
            await _context.Servicos.AddAsync(servico);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Servico>> GetServicosAsync()
        {
            return await _context.Servicos.ToListAsync();
        }

        public async Task<Servico?> GetServicoByIdAsync(int id)
        {
            return await _context.Servicos.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateServicoAsync(Servico servico)
        {
            var existente = await _context.Servicos.FindAsync(servico.Id);

            if (existente != null)
            {
                existente.Nome = servico.Nome;
                existente.Inclusao = servico.Inclusao;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteServicoAsync(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);

            if (servico != null)
            {
                _context.Servicos.Remove(servico);
                await _context.SaveChangesAsync();
            }
        }
    }
}
