using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Veterinaria.Infreaestructure.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaDetallesCita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Mascotas_IdMascota",
                table: "Citas");

            migrationBuilder.RenameColumn(
                name: "IdMascota",
                table: "Citas",
                newName: "IdDueno");

            migrationBuilder.RenameIndex(
                name: "IX_Citas_IdMascota",
                table: "Citas",
                newName: "IX_Citas_IdDueno");

            migrationBuilder.AlterColumn<string>(
                name: "NombreVeterinario",
                table: "Veterinarios",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "Mascotas",
                type: "character varying(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "NombreMascota",
                table: "Mascotas",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Especie",
                table: "Mascotas",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "NombreDueno",
                table: "Duenos",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40);

            migrationBuilder.CreateTable(
                name: "detalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CitaID = table.Column<int>(type: "integer", nullable: false),
                    MacotaId = table.Column<int>(type: "integer", nullable: false),
                    NombreMascota = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_detalles_Citas_CitaID",
                        column: x => x.CitaID,
                        principalTable: "Citas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_detalles_Mascotas_MacotaId",
                        column: x => x.MacotaId,
                        principalTable: "Mascotas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Mascota_Sexo",
                table: "Mascotas",
                sql: "\"Sexo\" IN('M', 'F')");

            migrationBuilder.CreateIndex(
                name: "IX_detalles_CitaID",
                table: "detalles",
                column: "CitaID");

            migrationBuilder.CreateIndex(
                name: "IX_detalles_MacotaId",
                table: "detalles",
                column: "MacotaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Duenos_IdDueno",
                table: "Citas",
                column: "IdDueno",
                principalTable: "Duenos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Duenos_IdDueno",
                table: "Citas");

            migrationBuilder.DropTable(
                name: "detalles");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Mascota_Sexo",
                table: "Mascotas");

            migrationBuilder.RenameColumn(
                name: "IdDueno",
                table: "Citas",
                newName: "IdMascota");

            migrationBuilder.RenameIndex(
                name: "IX_Citas_IdDueno",
                table: "Citas",
                newName: "IX_Citas_IdMascota");

            migrationBuilder.AlterColumn<string>(
                name: "NombreVeterinario",
                table: "Veterinarios",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "Mascotas",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1)",
                oldMaxLength: 1);

            migrationBuilder.AlterColumn<string>(
                name: "NombreMascota",
                table: "Mascotas",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Especie",
                table: "Mascotas",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "NombreDueno",
                table: "Duenos",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Mascotas_IdMascota",
                table: "Citas",
                column: "IdMascota",
                principalTable: "Mascotas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
