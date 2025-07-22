using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatosPesca.Migrations
{
    /// <inheritdoc />
    public partial class ModeloCapturaImagen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagenNombre",
                table: "Capturas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenNombre",
                table: "Capturas");
        }
    }
}
