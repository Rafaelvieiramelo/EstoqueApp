using LidyDecorApp.Domain.Interfaces;
using LidyDecorApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace LidyDecorApp.Infrastructure.Repository
{
    public class OrcamentosRepository : IOrcamentosRepository
    {
        private readonly LidyDecorDbContext _context;

        public OrcamentosRepository(LidyDecorDbContext context)
        {
            _context = context;
        }

        public async Task AddOrcamentosAsync(Orcamentos orcamentos)
        {
            await _context.Orcamentos.AddAsync(orcamentos);
            await _context.SaveChangesAsync();
        }

        public async Task<string?> GetNumeroUltimoOrcamentosAsync()
        {
            try
            {
                return await _context.Orcamentos.OrderByDescending(p => p.Id).Select(p => p.Numero).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<IEnumerable<Orcamentos>> GetOrcamentossAsync()
        {
            try
            {
                return await _context.Orcamentos
                    .Include(o => o.Clientes)
                    .Include(o => o.TipoEvento)
                    .Include(o => o.ProdutosOrcamento)
                        .ThenInclude(po => po.Produtoss) // Inclui informações dos Produtoss
                    .ToListAsync();
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public async Task<IEnumerable<TipoEventos>> GetTiposEventoAsync()
        {
            return await _context.TipoEventos.ToListAsync();
        }

        public async Task<Orcamentos> GetOrcamentosByIdAsync(int id)
        {
            return await _context.Orcamentos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateOrcamentosAsync(Orcamentos orcamentos)
        {

            var orcamentosExistente = await _context.Orcamentos.FindAsync(orcamentos.Id);

            if (orcamentosExistente != null)
            {
                orcamentosExistente.Numero = orcamentos.Numero;
                orcamentosExistente.TipoEventoId = orcamentos.TipoEventoId;
                orcamentosExistente.ClientesId = orcamentos.ClientesId;
                orcamentosExistente.Data = orcamentos.Data;
                orcamentosExistente.DataEvento = orcamentos.DataEvento;
                orcamentosExistente.Observacoes = orcamentos.Observacoes;
                orcamentosExistente.ValorTotal = orcamentos.ValorTotal;

                foreach (var item in orcamentos.ProdutosOrcamento.Where(p => p.Id == 0))
                {
                    item.Produtoss = null;
                    await _context.ProdutosOrcamento.AddAsync(item);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrcamentosAsync(int id)
        {
            var orcamentos = await _context.Orcamentos.FindAsync(id);

            if (orcamentos != null)
            {
                _context.Orcamentos.Remove(orcamentos);
                await _context.SaveChangesAsync();
            }
        }
    }
}

