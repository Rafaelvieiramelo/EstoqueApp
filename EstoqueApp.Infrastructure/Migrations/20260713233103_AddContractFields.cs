using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LidyDecorApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddContractFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CidadeContrato",
                table: "Orcamentos",
                type: "TEXT",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoEntrega",
                table: "Orcamentos",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormaPagamento",
                table: "Orcamentos",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PorcentagemSinal",
                table: "Orcamentos",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "TemaPacote",
                table: "Orcamentos",
                type: "TEXT",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ValorSinal",
                table: "Orcamentos",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CidadeContrato",
                table: "Orcamentos");

            migrationBuilder.DropColumn(
                name: "EnderecoEntrega",
                table: "Orcamentos");

            migrationBuilder.DropColumn(
                name: "FormaPagamento",
                table: "Orcamentos");

            migrationBuilder.DropColumn(
                name: "PorcentagemSinal",
                table: "Orcamentos");

            migrationBuilder.DropColumn(
                name: "TemaPacote",
                table: "Orcamentos");

            migrationBuilder.DropColumn(
                name: "ValorSinal",
                table: "Orcamentos");
        }
    }
}
