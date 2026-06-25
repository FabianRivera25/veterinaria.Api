using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veterinaria.Infreaestructure.Migrations
{
    /// <inheritdoc />
    public partial class correcionRelacionesUsuarioYFlujo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdUsuario",
                table: "Veterinarios",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdUsuario",
                table: "Duenos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdDueno",
                table: "Citas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarios_IdUsuario",
                table: "Veterinarios",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Duenos_IdUsuario",
                table: "Duenos",
                column: "IdUsuario");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Duenos_AspNetUsers_IdUsuario",
                table: "Duenos",
                column: "IdUsuario",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Veterinarios_AspNetUsers_IdUsuario",
                table: "Veterinarios",
                column: "IdUsuario",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Duenos_IdDueno",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Duenos_AspNetUsers_IdUsuario",
                table: "Duenos");

            migrationBuilder.DropForeignKey(
                name: "FK_Veterinarios_AspNetUsers_IdUsuario",
                table: "Veterinarios");

            migrationBuilder.DropIndex(
                name: "IX_Veterinarios_IdUsuario",
                table: "Veterinarios");

            migrationBuilder.DropIndex(
                name: "IX_Duenos_IdUsuario",
                table: "Duenos");

            migrationBuilder.DropIndex(
                name: "IX_Citas_IdDueno",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Veterinarios");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Duenos");

            migrationBuilder.DropColumn(
                name: "IdDueno",
                table: "Citas");
        }
    }
}
