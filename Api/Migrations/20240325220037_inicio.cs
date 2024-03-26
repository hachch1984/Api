using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DocumentoIdentidad = table.Column<string>(type: "char(8)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    Estado = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Persona",
                columns: new[] { "Id", "Apellidos", "DocumentoIdentidad", "Estado", "FechaNacimiento", "Nombres" },
                values: new object[,]
                {
                    { "1", "Perez", "12345678", "A", new DateTime(2024, 3, 25, 17, 0, 37, 438, DateTimeKind.Local).AddTicks(3574), "Juan" },
                    { "2", "Lopez", "87654321", "A", new DateTime(2024, 3, 24, 17, 0, 37, 438, DateTimeKind.Local).AddTicks(3590), "Maria" },
                    { "3", "Gomez", "45678912", "A", new DateTime(2024, 3, 23, 17, 0, 37, 438, DateTimeKind.Local).AddTicks(3592), "Carlos" },
                    { "4", "Garcia", "78912345", "A", new DateTime(2024, 3, 22, 17, 0, 37, 438, DateTimeKind.Local).AddTicks(3593), "Luis" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persona");
        }
    }
}
