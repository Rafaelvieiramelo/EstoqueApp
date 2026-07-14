using LidyDecorApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LidyDecorApp.Infrastructure.Services
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(LidyDecorDbContext context)
        {
            // 1. Semeia TipoEventos
            if (!await context.TipoEventos.AnyAsync())
            {
                var tipos = new[]
                {
                    new TipoEventos { Tipo = "Aniversario" },
                    new TipoEventos { Tipo = "Aniversario Infantil" },
                    new TipoEventos { Tipo = "Chá Revelação" },
                    new TipoEventos { Tipo = "Chá de Bebê" },
                    new TipoEventos { Tipo = "Casamento" }
                };
                await context.TipoEventos.AddRangeAsync(tipos);
                await context.SaveChangesAsync();
            }

            // 2. Semeia Usuarios
            if (!await context.Usuarios.AnyAsync())
            {
                var admin = new Usuarios
                {
                    Nome = "Admin",
                    Email = "admin@admin.com.br",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin"
                };

                var user = new Usuarios
                {
                    Nome = "User",
                    Email = "teste@usr.com.br",
                    SenhaHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "User"
                };

                await context.Usuarios.AddRangeAsync(admin, user);
                await context.SaveChangesAsync();
            }

            // 3. Semeia um Produto de exemplo
            if (!await context.Produtos.AnyAsync())
            {
                var sampleProduct = new Produtos
                {
                    Nome = "Enfeite com balões (Porte Pequeno)",
                    Descricao = "Decoração com balões de porte pequeno",
                    PrecoUnitario = 350.00m,
                    Quantidade = 3
                };
                await context.Produtos.AddAsync(sampleProduct);
                await context.SaveChangesAsync();
            }
        }
    }
}
