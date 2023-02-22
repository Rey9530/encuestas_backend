using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace encuestasbackend.Migrations
{
    /// <inheritdoc />
    public partial class addPreguntasRespuestas2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdPregunta",
                table: "CuestionarioPreguntas",
                newName: "IdCuestionario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdCuestionario",
                table: "CuestionarioPreguntas",
                newName: "IdPregunta");
        }
    }
}
