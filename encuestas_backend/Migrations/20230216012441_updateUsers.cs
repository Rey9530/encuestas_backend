using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace encuestasbackend.Migrations
{
    /// <inheritdoc />
    public partial class updateUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "daaaa",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "daaaa",
                table: "AspNetUsers");
        }
    }
}
