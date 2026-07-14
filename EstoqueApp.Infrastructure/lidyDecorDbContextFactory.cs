using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LidyDecorApp.Infrastructure
{
    public class lidyDecorDbContextFactory : IDesignTimeDbContextFactory<LidyDecorDbContext>
    {
        public LidyDecorDbContext CreateDbContext(string[] args)
        {
            var basePath = AppContext.BaseDirectory;
            var projectPath = Path.GetFullPath(Path.Combine(basePath, "..", "..", "..", "..", "ApiContorleEstoque"));
            if (!Directory.Exists(projectPath))
            {
                projectPath = Directory.GetCurrentDirectory();
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
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
