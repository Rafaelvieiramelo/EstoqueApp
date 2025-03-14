﻿// <auto-generated />
using System;
using LidyDecorApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LidyDecorApp.Infrastructure.Migrations
{
    [DbContext(typeof(LidyDecorDbContext))]
    partial class EstoqueDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("LidyDecorApp.Domain.Clientes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bairro")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cep")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cidade")
                        .HasColumnType("TEXT");

                    b.Property<string>("CpfCnpj")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("Estado")
                        .HasColumnType("TEXT");

                    b.Property<string>("Logradouro")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Numero")
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("LidyDecorApp.Domain.Orcamentos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientesId")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("Data")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("DataEvento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Observacoes")
                        .HasColumnType("TEXT");

                    b.Property<int>("TipoEventoId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClientesId");

                    b.HasIndex("TipoEventoId");

                    b.ToTable("Orcamentos");
                });

            modelBuilder.Entity("LidyDecorApp.Domain.Produtos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PrecoUnitario")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Produtoss", (string)null);
                });

            modelBuilder.Entity("LidyDecorApp.Domain.ProdutossOrcamentos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("Inclusao")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrcamentosId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProdutosId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OrcamentosId");

                    b.HasIndex("ProdutosId");

                    b.ToTable("ProdutossOrcamentos");
                });

            modelBuilder.Entity("LidyDecorApp.Domain.TipoEvento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TipoEvento");
                });

            modelBuilder.Entity("LidyDecorApp.Domain.Usuarios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("SenhaHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("LidyDecorApp.Domain.Orcamentos", b =>
                {
                    b.HasOne("LidyDecorApp.Domain.Clientes", "Clientes")
                        .WithMany()
                        .HasForeignKey("ClientesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LidyDecorApp.Domain.TipoEvento", "TipoEvento")
                        .WithMany()
                        .HasForeignKey("TipoEventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clientes");

                    b.Navigation("TipoEvento");
                });

            modelBuilder.Entity("LidyDecorApp.Domain.ProdutossOrcamentos", b =>
                {
                    b.HasOne("LidyDecorApp.Domain.Orcamentos", "Orcamentos")
                        .WithMany("ProdutossOrcamentos")
                        .HasForeignKey("OrcamentosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LidyDecorApp.Domain.Produtos", "Produtoss")
                        .WithMany("ProdutossOrcamentos")
                        .HasForeignKey("ProdutosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orcamentos");

                    b.Navigation("Produtoss");
                });

            modelBuilder.Entity("LidyDecorApp.Domain.Orcamentos", b =>
                {
                    b.Navigation("ProdutossOrcamentos");
                });

            modelBuilder.Entity("LidyDecorApp.Domain.Produtos", b =>
                {
                    b.Navigation("ProdutossOrcamentos");
                });
#pragma warning restore 612, 618
        }
    }
}
