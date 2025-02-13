using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microondas.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCamposCustomizadoECaractere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Caractere",
                table: "Programas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Customizado",
                table: "Programas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caractere",
                table: "Programas");

            migrationBuilder.DropColumn(
                name: "Customizado",
                table: "Programas");
        }
    }
}
