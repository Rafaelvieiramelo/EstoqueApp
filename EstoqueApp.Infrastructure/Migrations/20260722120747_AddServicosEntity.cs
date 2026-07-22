using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LidyDecorApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddServicosEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServicoId",
                table: "Orcamentos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Inclusao = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orcamentos_ServicoId",
                table: "Orcamentos",
                column: "ServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orcamentos_Servicos_ServicoId",
                table: "Orcamentos",
                column: "ServicoId",
                principalTable: "Servicos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orcamentos_Servicos_ServicoId",
                table: "Orcamentos");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_Orcamentos_ServicoId",
                table: "Orcamentos");

            migrationBuilder.DropColumn(
                name: "ServicoId",
                table: "Orcamentos");
        }
    }
}
