using EstoqueApp.Domain.Interfaces;
using EstoqueApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Infrastructure.Repository
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly EstoqueDbContext _context;

        public FornecedorRepository(EstoqueDbContext context)
        {
            _context = context;
        }

        public async Task AddFornecedorAsync(Fornecedor fornecedor)
        {
            await _context.Fornecedores.AddAsync(fornecedor);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Fornecedor>> GetFornecedoresAsync()
        {
            return await _context.Fornecedores.ToListAsync();
        }

        public async Task<Fornecedor> GetFornecedorByIdAsync(int id)
        {
            return await _context.Fornecedores
                                 .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task UpdateFornecedorAsync(Fornecedor fornecedor)
        {
            var fornecedorExistente = await _context.Fornecedores.FindAsync(fornecedor.Id);

            if (fornecedorExistente != null)
            {
                fornecedorExistente.Nome = fornecedor.Nome;
                fornecedorExistente.CNPJ = fornecedor.CNPJ;
                fornecedorExistente.Endereco = fornecedor.Endereco;
                fornecedorExistente.Telefone = fornecedor.Telefone;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteFornecedorAsync(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);

            if (fornecedor != null)
            {
                _context.Fornecedores.Remove(fornecedor);
                await _context.SaveChangesAsync();
            }
        }
    }
}