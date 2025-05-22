using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatosPesca.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Capturas",
                columns: table => new
                {
                    CapturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    NombreEspecie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tamaño = table.Column<double>(type: "float", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Localidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraAproximada = table.Column<int>(type: "int", nullable: false),
                    Zona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profundidad = table.Column<int>(type: "int", nullable: false),
                    Oleaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TiempoClimatico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaridadAgua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstiloPesca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anzuelo = table.Column<bool>(type: "bit", nullable: false),
                    TamañoAnzuelo = table.Column<int>(type: "int", nullable: true),
                    Gusano = table.Column<bool>(type: "bit", nullable: false),
                    TipoGusano = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TamañoHilo = table.Column<double>(type: "float", nullable: false),
                    TamañoBajo = table.Column<double>(type: "float", nullable: true),
                    Señuelo = table.Column<bool>(type: "bit", nullable: false),
                    TipoSeñuelo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capturas", x => x.CapturaId);
                    table.ForeignKey(
                        name: "FK_Capturas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Capturas_UsuarioId",
                table: "Capturas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Capturas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
