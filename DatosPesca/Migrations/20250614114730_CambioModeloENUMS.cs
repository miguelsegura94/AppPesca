using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatosPesca.Migrations
{
    /// <inheritdoc />
    public partial class CambioModeloENUMS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoGusano",
                table: "Capturas",
                newName: "TipoCebo");

            migrationBuilder.RenameColumn(
                name: "Gusano",
                table: "Capturas",
                newName: "Cebo");

            migrationBuilder.AlterColumn<string>(
                name: "TamañoAnzuelo",
                table: "Capturas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoCebo",
                table: "Capturas",
                newName: "TipoGusano");

            migrationBuilder.RenameColumn(
                name: "Cebo",
                table: "Capturas",
                newName: "Gusano");

            migrationBuilder.AlterColumn<int>(
                name: "TamañoAnzuelo",
                table: "Capturas",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
