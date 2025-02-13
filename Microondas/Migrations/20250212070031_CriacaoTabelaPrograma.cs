using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microondas.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoTabelaPrograma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Programas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alimento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tempo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Potencia = table.Column<int>(type: "int", nullable: true),
                    Instrucao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Programas");
        }
    }
}
