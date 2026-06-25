using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veterinaria.Infreaestructure.Migrations
{
    /// <inheritdoc />
    public partial class correciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Duenos_DuenoId",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_DuenoId",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "DuenoId",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "IdDueno",
                table: "Citas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DuenoId",
                table: "Citas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdDueno",
                table: "Citas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Citas_DuenoId",
                table: "Citas",
                column: "DuenoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Duenos_DuenoId",
                table: "Citas",
                column: "DuenoId",
                principalTable: "Duenos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
