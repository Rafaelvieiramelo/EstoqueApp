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
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            // Obter a string de conexão
            var rawConnectionString = configuration.GetConnectionString("DefaultConnection") 
                                      ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                                      ?? Environment.GetEnvironmentVariable("DATABASE_URL");

            var connectionString = ConvertPostgresUrlToConnectionString(rawConnectionString);

            // Configurar o DbContext com a string de conexão
            var optionsBuilder = new DbContextOptionsBuilder<LidyDecorDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new LidyDecorDbContext(optionsBuilder.Options);
        }

        private static string ConvertPostgresUrlToConnectionString(string? url)
        {
            if (string.IsNullOrWhiteSpace(url)) return string.Empty;
            if (!url.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase)) return url;

            try
            {
                var uri = new Uri(url);
                var userInfo = uri.UserInfo.Split(':');
                var username = userInfo[0];
                var password = userInfo.Length > 1 ? userInfo[1] : "";
                var host = uri.Host;
                var port = uri.Port > 0 ? uri.Port : 5432;
                var database = uri.AbsolutePath.TrimStart('/');

                return $"Host={host};Port={port};Database={database};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=true;";
            }
            catch
            {
                return url;
            }
        }
    }
}
