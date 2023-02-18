using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace encuestasbackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIDCuestionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cuestionario_CuestionarioIdCuestionario",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "IdCuestionario",
                table: "Cuestionario",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CuestionarioIdCuestionario",
                table: "AspNetUsers",
                newName: "CuestionarioId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CuestionarioIdCuestionario",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CuestionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cuestionario_CuestionarioId",
                table: "AspNetUsers",
                column: "CuestionarioId",
                principalTable: "Cuestionario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cuestionario_CuestionarioId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cuestionario",
                newName: "IdCuestionario");

            migrationBuilder.RenameColumn(
                name: "CuestionarioId",
                table: "AspNetUsers",
                newName: "CuestionarioIdCuestionario");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CuestionarioId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CuestionarioIdCuestionario");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cuestionario_CuestionarioIdCuestionario",
                table: "AspNetUsers",
                column: "CuestionarioIdCuestionario",
                principalTable: "Cuestionario",
                principalColumn: "IdCuestionario");
        }
    }
}
