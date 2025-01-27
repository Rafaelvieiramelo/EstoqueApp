using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EstoqueApp.Infrastructure
{
    public class EstoqueDbContextFactory : IDesignTimeDbContextFactory<EstoqueDbContext>
    {
        public EstoqueDbContext CreateDbContext(string[] args)
        {
            // Carregar as configurações do arquivo appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "C:\\Users\\rafael.melo\\source\\repos\\ControleEstoque\\ApiContorleEstoque"))
                .AddJsonFile("appsettings.json")
                .Build();

            // Obter a string de conexão
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configurar o DbContext com a string de conexão
            var optionsBuilder = new DbContextOptionsBuilder<EstoqueDbContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new EstoqueDbContext(optionsBuilder.Options);
        }
    }
}
