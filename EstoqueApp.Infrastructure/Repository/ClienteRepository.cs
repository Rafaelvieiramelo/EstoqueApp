using LidyDecorApp.Domain.Interfaces;
using LidyDecorApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace LidyDecorApp.Infrastructure.Repository
{
    public class ClientesRepository : IClientesRepository
    {
        private readonly LidyDecorDbContext _context;

        public ClientesRepository(LidyDecorDbContext context)
        {
            _context = context;
        }

        public async Task AddClientesAsync(Clientes clientes)
        {
            await _context.Clientes.AddAsync(clientes);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Clientes>> GetClientessAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Clientes> GetClientesByIdAsync(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateClientesAsync(Clientes clientes)
        {
            var clientesExistente = await _context.Clientes.FindAsync(clientes.Id);

            if (clientesExistente != null)
            {
                clientesExistente.Nome = clientes.Nome;
                clientesExistente.Email = clientes.Email;
                clientesExistente.Telefone = clientes.Telefone;
                clientesExistente.CpfCnpj = clientes.CpfCnpj;
                clientesExistente.Logradouro = clientes.Logradouro;
                clientesExistente.Numero = clientes.Numero;
                clientesExistente.Bairro = clientes.Bairro;
                clientesExistente.Cidade = clientes.Cidade;
                clientesExistente.Estado = clientes.Estado;
                clientesExistente.Cep = clientes.Cep;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteClientesAsync(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);

            if (clientes != null)
            {
                _context.Clientes.Remove(clientes);
                await _context.SaveChangesAsync();
            }
        }
    }
}
