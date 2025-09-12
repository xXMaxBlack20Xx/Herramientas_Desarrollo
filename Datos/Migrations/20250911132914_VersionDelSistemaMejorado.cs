using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datos.Migrations
{
    /// <inheritdoc />
    public partial class VersionDelSistemaMejorado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CEF");

            migrationBuilder.CreateTable(
                name: "Carreras",
                schema: "CEF",
                columns: table => new
                {
                    IdCarrera = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaveCarrera = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    NombreCarrera = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AliasCarrera = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EstadoCarrera = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.IdCarrera);
                });

            migrationBuilder.CreateTable(
                name: "PlanesEstudio",
                schema: "CEF",
                columns: table => new
                {
                    IdPlanEstudio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanEstudio = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    TotalCreditos = table.Column<int>(type: "int", nullable: false),
                    CreditosOptativos = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    CreditosObligatorios = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    PerfilDeIngreso = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    PerfelDeEgreso = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    CampoOcupacional = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    EstadoPlanEstudio = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IdCarrera = table.Column<int>(type: "int", nullable: false),
                    IdNivelAcademico = table.Column<int>(type: "int", nullable: false),
                    CarreraIdCarrera = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanesEstudio", x => x.IdPlanEstudio);
                    table.ForeignKey(
                        name: "FK_PlanesEstudio_Carreras_CarreraIdCarrera",
                        column: x => x.CarreraIdCarrera,
                        principalSchema: "CEF",
                        principalTable: "Carreras",
                        principalColumn: "IdCarrera",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanesEstudio_CarreraIdCarrera",
                schema: "CEF",
                table: "PlanesEstudio",
                column: "CarreraIdCarrera");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanesEstudio",
                schema: "CEF");

            migrationBuilder.DropTable(
                name: "Carreras",
                schema: "CEF");
        }
    }
}
