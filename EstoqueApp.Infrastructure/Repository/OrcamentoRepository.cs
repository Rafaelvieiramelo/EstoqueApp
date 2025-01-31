using EstoqueApp.Domain.Interfaces;
using EstoqueApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Infrastructure.Repository
{
    public class OrcamentoRepository : IOrcamentoRepository
    {
        private readonly EstoqueDbContext _context;

        public OrcamentoRepository(EstoqueDbContext context)
        {
            _context = context;
        }

        public async Task AddOrcamentoAsync(Orcamento orcamento)
        {
            await _context.Orcamento.AddAsync(orcamento);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Orcamento>> GetOrcamentosAsync()
        {
            return await _context.Orcamento
                .Include(o => o.Cliente)
                .Include(o => o.TipoEvento)
                .ToListAsync();
        }

        public async Task<IEnumerable<TipoEvento>> GetTiposEventoAsync()
        {
            return await _context.TipoEvento.ToListAsync();
        }

        public async Task<Orcamento> GetOrcamentoByIdAsync(int id)
        {
            return await _context.Orcamento.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateOrcamentoAsync(Orcamento orcamento)
        {

            var orcamentoExistente = await _context.Orcamento.FindAsync(orcamento.Id);

            if (orcamentoExistente != null)
            {
                orcamentoExistente.Numero = orcamento.Numero;
                orcamentoExistente.TipoEventoId = orcamento.TipoEventoId;
                orcamentoExistente.ClienteId = orcamento.ClienteId;
                orcamentoExistente.Data = orcamento.Data;
                orcamentoExistente.DataEvento = orcamento.DataEvento;
                orcamentoExistente.Observacoes = orcamento.Observacoes;
                orcamentoExistente.ValorTotal = orcamento.ValorTotal;

                orcamentoExistente.ProdutosOrcamento = orcamento.ProdutosOrcamento;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrcamentoAsync(int id)
        {
            var orcamento = await _context.Orcamento.FindAsync(id);

            if (orcamento != null)
            {
                _context.Orcamento.Remove(orcamento);
                await _context.SaveChangesAsync();
            }
        }
    }
}

