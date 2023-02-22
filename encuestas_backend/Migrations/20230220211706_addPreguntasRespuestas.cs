using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace encuestasbackend.Migrations
{
    /// <inheritdoc />
    public partial class addPreguntasRespuestas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CuestionarioPreguntas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Pregunta = table.Column<string>(type: "text", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdPregunta = table.Column<int>(type: "integer", nullable: false),
                    CuestionarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuestionarioPreguntas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CuestionarioPreguntas_Cuestionario_CuestionarioId",
                        column: x => x.CuestionarioId,
                        principalTable: "Cuestionario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CuestionarioPreguntasRespuestas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPregunta = table.Column<int>(type: "integer", nullable: false),
                    PreguntaId = table.Column<int>(type: "integer", nullable: true),
                    Respuesta = table.Column<int>(type: "integer", nullable: false),
                    EsCorrecta = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuestionarioPreguntasRespuestas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CuestionarioPreguntasRespuestas_CuestionarioPreguntas_Pregu~",
                        column: x => x.PreguntaId,
                        principalTable: "CuestionarioPreguntas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CuestionarioPreguntas_CuestionarioId",
                table: "CuestionarioPreguntas",
                column: "CuestionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CuestionarioPreguntasRespuestas_PreguntaId",
                table: "CuestionarioPreguntasRespuestas",
                column: "PreguntaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuestionarioPreguntasRespuestas");

            migrationBuilder.DropTable(
                name: "CuestionarioPreguntas");
        }
    }
}
