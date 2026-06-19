using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ENGER.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnEmployeeAndPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CARGOS",
                columns: table => new
                {
                    CD_CARGO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DS_CARGO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CD_EMPRESA = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARGOS", x => x.CD_CARGO);
                    table.ForeignKey(
                        name: "FK_CARGOS_EMPRESAS_CD_EMPRESA",
                        column: x => x.CD_EMPRESA,
                        principalTable: "EMPRESAS",
                        principalColumn: "CD_EMPRESA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FUNCIONARIOS",
                columns: table => new
                {
                    CD_FUNCIONARIO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NM_FUNCIONARIO = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    RG = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    DT_NASCIMENTO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DT_ADMISSAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NR_TELEFONE = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    NR_CELULAR = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    EMAIL = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DT_ENTRADA = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DT_ATUALIZACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CD_EMPRESA = table.Column<int>(type: "integer", nullable: false),
                    CD_CARGO = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FUNCIONARIOS", x => x.CD_FUNCIONARIO);
                    table.ForeignKey(
                        name: "FK_FUNCIONARIOS_CARGOS_CD_CARGO",
                        column: x => x.CD_CARGO,
                        principalTable: "CARGOS",
                        principalColumn: "CD_CARGO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FUNCIONARIOS_EMPRESAS_CD_EMPRESA",
                        column: x => x.CD_EMPRESA,
                        principalTable: "EMPRESAS",
                        principalColumn: "CD_EMPRESA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CARGOS_CD_EMPRESA",
                table: "CARGOS",
                column: "CD_EMPRESA");

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIOS_CD_CARGO",
                table: "FUNCIONARIOS",
                column: "CD_CARGO");

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIOS_CD_EMPRESA",
                table: "FUNCIONARIOS",
                column: "CD_EMPRESA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FUNCIONARIOS");

            migrationBuilder.DropTable(
                name: "CARGOS");
        }
    }
}
