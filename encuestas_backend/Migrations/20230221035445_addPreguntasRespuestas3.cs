using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace encuestasbackend.Migrations
{
    /// <inheritdoc />
    public partial class addPreguntasRespuestas3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Respuesta",
                table: "CuestionarioPreguntasRespuestas",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Respuesta",
                table: "CuestionarioPreguntasRespuestas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
