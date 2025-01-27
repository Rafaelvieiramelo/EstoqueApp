using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstoqueApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTeste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SobreNome",
                table: "Usuario",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SobreNome",
                table: "Usuario");
        }
    }
}
