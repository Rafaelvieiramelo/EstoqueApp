using EstoqueApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Infrastructure
{
    public class EstoqueDbContext : DbContext
    {
        public EstoqueDbContext(DbContextOptions<EstoqueDbContext> options)
        : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<MovimentoEstoque> MovimentosEstoque { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Orcamento> Orcamento { get; set; }
        public DbSet<ProdutosOrcamento> ProdutosOrcamento { get; set; }
        public DbSet<TipoEvento> TipoEvento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Produto>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.SenhaHash).IsRequired();
                entity.Property(e => e.Role).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(150);
            });

            modelBuilder.Entity<Orcamento>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Data)
                    .IsRequired();

                // Configurando o relacionamento com TipoEvento
                entity.HasOne(e => e.TipoEvento)  // Orcamento tem um TipoEvento
                    .WithMany()                    // TipoEvento pode estar relacionado com vários Orçamentos
                    .HasForeignKey(e => e.TipoEventoId)  // A chave estrangeira para TipoEvento
                    .IsRequired();  // Pode ser obrigatório (caso o relacionamento seja obrigatório)
            });

            modelBuilder.Entity<TipoEvento>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<ProdutosOrcamento>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.OrcamentoId, e.ProdutoId });  // Chave composta

                entity.HasOne(po => po.Orcamento)
                    .WithMany(o => o.ProdutosOrcamento)
                    .HasForeignKey(po => po.OrcamentoId)
                    .IsRequired();

                entity.HasOne(po => po.Produto)
                    .WithMany(p => p.ProdutosOrcamento)
                    .HasForeignKey(po => po.ProdutoId)
                    .IsRequired();
            });
        }
    }
}
