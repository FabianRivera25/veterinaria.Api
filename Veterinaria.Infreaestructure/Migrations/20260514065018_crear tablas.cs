using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Veterinaria.Infreaestructure.Migrations
{
    /// <inheritdoc />
    public partial class creartablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dueños",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreDueño = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    DNI = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    FechaRegistro = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dueños", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veterinarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreVeterinario = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Especialidad = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    DNI = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FechaRegistro = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veterinarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mascotas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdDueño = table.Column<int>(type: "integer", nullable: false),
                    NombreMascota = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Especie = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Raza = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Edad = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Sexo = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    FechaRegistro = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mascotas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mascotas_Dueños_IdDueño",
                        column: x => x.IdDueño,
                        principalTable: "Dueños",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdMascota = table.Column<int>(type: "integer", nullable: false),
                    IdVeterinario = table.Column<int>(type: "integer", nullable: false),
                    Motivo = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    FechaCita = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Estado = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    FechaRegistro = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.Id);
                    table.CheckConstraint("CK_Cita_Estado", "\"Estado\" IN('Activo', 'Inactivo')");
                    table.ForeignKey(
                        name: "FK_Citas_Mascotas_IdMascota",
                        column: x => x.IdMascota,
                        principalTable: "Mascotas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Citas_Veterinarios_IdVeterinario",
                        column: x => x.IdVeterinario,
                        principalTable: "Veterinarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citas_FechaCita",
                table: "Citas",
                column: "FechaCita",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Citas_IdMascota",
                table: "Citas",
                column: "IdMascota");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_IdVeterinario",
                table: "Citas",
                column: "IdVeterinario");

            migrationBuilder.CreateIndex(
                name: "IX_Dueños_DNI",
                table: "Dueños",
                column: "DNI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_IdDueño",
                table: "Mascotas",
                column: "IdDueño");

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarios_DNI",
                table: "Veterinarios",
                column: "DNI",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Mascotas");

            migrationBuilder.DropTable(
                name: "Veterinarios");

            migrationBuilder.DropTable(
                name: "Dueños");
        }
    }
}
