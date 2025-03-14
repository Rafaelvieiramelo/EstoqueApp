using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LidyDecorApp.Infrastructure
{
    public class lidyDecorDbContextFactory : IDesignTimeDbContextFactory<LidyDecorDbContext>
    {
        public LidyDecorDbContext CreateDbContext(string[] args)
        {
            // Carregar as configurações do arquivo appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "C:\\Users\\rafael.melo\\source\\repos\\ControleEstoque\\ApiContorleEstoque"))
                .AddJsonFile("appsettings.json")
                .Build();

            // Obter a string de conexão
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configurar o DbContext com a string de conexão
            var optionsBuilder = new DbContextOptionsBuilder<LidyDecorDbContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new LidyDecorDbContext(optionsBuilder.Options);
        }
    }
}
