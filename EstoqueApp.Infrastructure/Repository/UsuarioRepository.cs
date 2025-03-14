using LidyDecorApp.Domain.Interfaces;
using LidyDecorApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace LidyDecorApp.Infrastructure.Repository
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly LidyDecorDbContext _context;

        public UsuariosRepository(LidyDecorDbContext context)
        {
            _context = context;
        }

        public async Task AddUsuariosAsync(Usuarios usuarios)
        {
            await _context.Usuarios.AddAsync(usuarios);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuarios?> GetUsuariosByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<IEnumerable<Usuarios>> GetUsuariossAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuarios> GetUsuariosByIdAsync(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateUsuariosAsync(Usuarios usuarios)
        {
            var usuariosExistente = await _context.Usuarios.FindAsync(usuarios.Id);

            if (usuariosExistente != null)
            {
                usuariosExistente.Nome = usuarios.Nome;
                usuariosExistente.Email = usuarios.Email;
                usuariosExistente.Role = usuarios.Role;
                usuariosExistente.SenhaHash = usuarios.SenhaHash;
                usuariosExistente.Ativo = usuarios.Ativo;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUsuariosAsync(int id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);

            if (usuarios != null)
            {
                _context.Usuarios.Remove(usuarios);
                await _context.SaveChangesAsync();
            }
        }
    }
}
