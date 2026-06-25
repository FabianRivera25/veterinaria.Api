using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veterinaria.Infreaestructure.Migrations
{
    /// <inheritdoc />
    public partial class correccine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Duenos_IdDueno",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_IdDueno",
                table: "Citas");

            migrationBuilder.AddColumn<int>(
                name: "DuenoId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Citas_IdDueno",
                table: "Citas",
                column: "IdDueno");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Duenos_IdDueno",
                table: "Citas",
                column: "IdDueno",
                principalTable: "Duenos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
