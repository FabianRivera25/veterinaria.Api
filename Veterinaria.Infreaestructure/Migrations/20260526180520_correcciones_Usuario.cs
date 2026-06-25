using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Veterinaria.Infreaestructure.Migrations
{
    /// <inheritdoc />
    public partial class correcciones_Usuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_Dueños_IdDueño",
                table: "Mascotas");

            migrationBuilder.DropTable(
                name: "Dueños");

            migrationBuilder.RenameColumn(
                name: "IdDueño",
                table: "Mascotas",
                newName: "IdDueno");

            migrationBuilder.RenameIndex(
                name: "IX_Mascotas_IdDueño",
                table: "Mascotas",
                newName: "IX_Mascotas_IdDueno");

            migrationBuilder.CreateTable(
                name: "Duenos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreDueno = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    DNI = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    FechaRegistro = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duenos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Duenos_DNI",
                table: "Duenos",
                column: "DNI",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_Duenos_IdDueno",
                table: "Mascotas",
                column: "IdDueno",
                principalTable: "Duenos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_Duenos_IdDueno",
                table: "Mascotas");

            migrationBuilder.DropTable(
                name: "Duenos");

            migrationBuilder.RenameColumn(
                name: "IdDueno",
                table: "Mascotas",
                newName: "IdDueño");

            migrationBuilder.RenameIndex(
                name: "IX_Mascotas_IdDueno",
                table: "Mascotas",
                newName: "IX_Mascotas_IdDueño");

            migrationBuilder.CreateTable(
                name: "Dueños",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DNI = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    FechaRegistro = table.Column<DateOnly>(type: "date", nullable: false),
                    NombreDueño = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dueños", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dueños_DNI",
                table: "Dueños",
                column: "DNI",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_Dueños_IdDueño",
                table: "Mascotas",
                column: "IdDueño",
                principalTable: "Dueños",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
