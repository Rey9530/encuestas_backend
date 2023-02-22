using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace encuestasbackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdsForaneos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cuestionario_CuestionarioId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CuestionarioId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdPregunta",
                table: "CuestionarioPreguntasRespuestas");

            migrationBuilder.DropColumn(
                name: "IdCuestionario",
                table: "CuestionarioPreguntas");

            migrationBuilder.DropColumn(
                name: "CuestionarioId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "idUsuarioCreador",
                table: "Cuestionario",
                newName: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuestionario_UsuarioId",
                table: "Cuestionario",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuestionario_AspNetUsers_UsuarioId",
                table: "Cuestionario",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuestionario_AspNetUsers_UsuarioId",
                table: "Cuestionario");

            migrationBuilder.DropIndex(
                name: "IX_Cuestionario_UsuarioId",
                table: "Cuestionario");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Cuestionario",
                newName: "idUsuarioCreador");

            migrationBuilder.AddColumn<int>(
                name: "IdPregunta",
                table: "CuestionarioPreguntasRespuestas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdCuestionario",
                table: "CuestionarioPreguntas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CuestionarioId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CuestionarioId",
                table: "AspNetUsers",
                column: "CuestionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cuestionario_CuestionarioId",
                table: "AspNetUsers",
                column: "CuestionarioId",
                principalTable: "Cuestionario",
                principalColumn: "Id");
        }
    }
}
