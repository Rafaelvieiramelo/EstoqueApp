using EstoqueApp.Domain.Interfaces;
using EstoqueApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Infrastructure.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly EstoqueDbContext _context;

        public UsuarioRepository(EstoqueDbContext context)
        {
            _context = context;
        }

        public async Task AddUsuarioAsync(Usuario usuario)
        {
            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> GetUsuarioByEmailAsync(string email)
        {
            return await _context.Usuario.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            return await _context.Usuario.ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuario.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateUsuarioAsync(Usuario usuario)
        {
            var usuarioExistente = await _context.Usuario.FindAsync(usuario.Id);

            if (usuarioExistente != null)
            {
                usuarioExistente.Nome = usuario.Nome;
                usuarioExistente.Email = usuario.Email;
                usuarioExistente.Role = usuario.Role;
                usuarioExistente.SenhaHash = usuario.SenhaHash;
                usuarioExistente.Ativo = usuario.Ativo;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
