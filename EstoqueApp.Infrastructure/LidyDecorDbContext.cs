using LidyDecorApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace LidyDecorApp.Infrastructure
{
    public class LidyDecorDbContext : DbContext
    {
        public LidyDecorDbContext(DbContextOptions<LidyDecorDbContext> options)
        : base(options)
        {
        }

        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Orcamentos> Orcamentos { get; set; }
        public DbSet<ProdutosOrcamento> ProdutosOrcamento { get; set; }
        public DbSet<TipoEventos> TipoEventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produtos>()
                .ToTable("Produtos")
                .HasKey(p => p.Id);

            modelBuilder.Entity<Produtos>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.SenhaHash).IsRequired();
                entity.Property(e => e.Role).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(150);
            });

            modelBuilder.Entity<Orcamentos>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Data)
                    .IsRequired();

                entity.HasOne(e => e.TipoEvento)  
                    .WithMany()
                    .HasForeignKey(e => e.TipoEventoId)
                    .IsRequired();
            });

            modelBuilder.Entity<TipoEventos>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ProdutosOrcamento>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.HasOne(po => po.Orcamentos)
                    .WithMany(o => o.ProdutosOrcamento)
                    .HasForeignKey(po => po.OrcamentosId)
                    .IsRequired();

                entity.HasOne(po => po.Produtoss)
                    .WithMany(p => p.ProdutosOrcamento)
                    .HasForeignKey(po => po.ProdutosId)
                    .IsRequired();
            });
        }
    }
}
