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
            
            modelBuilder.Entity<Produto>()
                .HasKey(p => p.Id); 

            modelBuilder.Entity<Produto>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
        public EstoqueDbContext(DbContextOptions<EstoqueDbContext> options)
            : base(options)
        {
        }
    }
}
