using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veterinaria.Infreaestructure.Migrations
{
    /// <inheritdoc />
    public partial class correcciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Experiencia",
                table: "RefreshTokens",
                newName: "Expiracion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Expiracion",
                table: "RefreshTokens",
                newName: "Experiencia");
        }
    }
}
