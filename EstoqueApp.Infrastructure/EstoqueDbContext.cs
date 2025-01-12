using EstoqueApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Infrastructure
{
    public class EstoqueDbContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<MovimentoEstoque> MovimentosEstoque { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura o Produto
            modelBuilder.Entity<Produto>()
                .HasKey(p => p.Id);  // Definindo a chave primária

            modelBuilder.Entity<Produto>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();  // Garante que o Id será auto-incrementado
        }

        // Construtor que recebe as opções de configuração, como a string de conexão
        public EstoqueDbContext(DbContextOptions<EstoqueDbContext> options)
            : base(options)
        {
        }

        // Não é necessário o método OnConfiguring agora, pois a configuração vem do DI container
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var caminhoBanco = @"C:\Users\rafael.melo\source\repos\ControleEstoque\Data\estoque.db";
        //    optionsBuilder.UseSqlite($"Data Source={caminhoBanco}");
        //}
    }
}
