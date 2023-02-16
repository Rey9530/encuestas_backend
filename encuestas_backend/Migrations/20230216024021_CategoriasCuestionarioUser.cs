using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace encuestasbackend.Migrations
{
    /// <inheritdoc />
    public partial class CategoriasCuestionarioUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Categorias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CuestionarioIdCuestionario",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cuestionario",
                columns: table => new
                {
                    IdCuestionario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false),
                    idUsuarioCreador = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuestionario", x => x.IdCuestionario);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CuestionarioIdCuestionario",
                table: "AspNetUsers",
                column: "CuestionarioIdCuestionario");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cuestionario_CuestionarioIdCuestionario",
                table: "AspNetUsers",
                column: "CuestionarioIdCuestionario",
                principalTable: "Cuestionario",
                principalColumn: "IdCuestionario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cuestionario_CuestionarioIdCuestionario",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cuestionario");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CuestionarioIdCuestionario",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "CuestionarioIdCuestionario",
                table: "AspNetUsers");
        }
    }
}
