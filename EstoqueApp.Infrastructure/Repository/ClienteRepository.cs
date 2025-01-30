using EstoqueApp.Domain.Interfaces;
using EstoqueApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Infrastructure.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly EstoqueDbContext _context;

        public ClienteRepository(EstoqueDbContext context)
        {
            _context = context;
        }

        public async Task AddClienteAsync(Cliente cliente)
        {
            await _context.Cliente.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            return await _context.Cliente.ToListAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            return await _context.Cliente.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            var clienteExistente = await _context.Cliente.FindAsync(cliente.Id);

            if (clienteExistente != null)
            {
                clienteExistente.Nome = cliente.Nome;
                clienteExistente.Email = cliente.Email;
                clienteExistente.Telefone = cliente.Telefone;
                clienteExistente.CpfCnpj = cliente.CpfCnpj;
                clienteExistente.Logradouro = cliente.Logradouro;
                clienteExistente.Numero = cliente.Numero;
                clienteExistente.Bairro = cliente.Bairro;
                clienteExistente.Cidade = cliente.Cidade;
                clienteExistente.Estado = cliente.Estado;
                clienteExistente.Cep = cliente.Cep;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteClienteAsync(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }
}
